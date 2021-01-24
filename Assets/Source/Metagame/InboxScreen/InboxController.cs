using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Metagame.InboxScreen
{
    public class InboxController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private InboxMessagePrefabController messagePrefab;

        [Inject] private InboxService inboxService;
        [Inject] private SignalBus signalBus;

        private void Start()
        {
            AddMessages(inboxService.Messages);
            signalBus.Subscribe<InboxSignal>(ConsumeInboxSignal);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<InboxSignal>(ConsumeInboxSignal);
        }

        private void ConsumeInboxSignal(InboxSignal signal)
        {
            if (signal.NewMessages != null)
            {
                AddMessages(signal.NewMessages);
            }
        }

        private void AddMessages(List<InboxMessage> messages) {
            messages.ForEach(message =>
            {
                var prefab = Instantiate(messagePrefab, canvas);
                prefab.SetMessage(message);
            });
        }
    }
}