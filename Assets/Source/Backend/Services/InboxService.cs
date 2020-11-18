using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class InboxService
    {
        public List<InboxMessage> Messages { get; private set; }
        
        [Inject] private ServerAPI serverAPI;
        private readonly SignalBus signalBus;

        public InboxService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public void Claim(InboxMessage message, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/inbox/claim/{message.id}", null, onSuccess);
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.inboxMessages != null)
            {
                if (Messages == null)
                {
                    Messages = data.inboxMessages;
                }
                else
                {
                    foreach (var message in data.inboxMessages)
                    {
                        Update(message);
                    }
                }
            }

            if (data.inboxMessageDeleted != null)
            {
                var idx = Messages.FindIndex(m => m.id == data.inboxMessageDeleted);
                if (idx >= 0)
                {
                    Messages.RemoveAt(idx);
                }
            }

            if (data.inboxMessages != null || data.inboxMessageDeleted != null)
            {
                signalBus.Fire(new InboxSignal(data.inboxMessages, data.inboxMessageDeleted));
            }
        }

        private void Update(InboxMessage message)
        {
            var idx = Messages.FindIndex(m => m.id == message.id);
            if (idx >= 0)
            {
                Messages[idx] = message;
            }
            else
            {
                Messages.Add(message);
            }
        }
    }
}