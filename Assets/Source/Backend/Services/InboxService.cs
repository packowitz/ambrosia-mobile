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

        public InboxService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
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