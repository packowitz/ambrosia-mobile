using System;
using Backend.Models;
using Backend.Services;
using Backend.Signal;
using TMPro;
using UnityEngine;
using Utils;
using Zenject;

namespace Metagame.InboxScreen
{
    public class InboxMessagePrefabController: MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text timeLeftText;
        [SerializeField] private ButtonController claimButton;
        [SerializeField] private RectTransform itemsCanvas;
        [SerializeField] private LootItemPrefabController itemPrefab;

        [Inject] private InboxService inboxService;
        [Inject] private ResourcesService resourcesService;
        [Inject] private SignalBus signalBus;

        private InboxMessage message;

        public void SetMessage(InboxMessage m)
        {
            message = m;
            description.text = message.message;
            
            message.items.ForEach(item =>
            {
                var prefab = Instantiate(itemPrefab, itemsCanvas);
                prefab.SetInboxItem(item, itemsCanvas.rect.height);
            });
            
            claimButton.AddClickListener(() =>
            {
                claimButton.ShowIndicator();
                inboxService.Claim(message, data =>
                {
                    Destroy(gameObject);
                });
            });
            CheckClaimButton();
            signalBus.Subscribe<ResourcesSignal>(CheckClaimButton);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<ResourcesSignal>(CheckClaimButton);
        }

        private void CheckClaimButton()
        {
            claimButton.SetInteractable(message.items.Exists(CanClaimItem));
        }

        private bool CanClaimItem(InboxMessageItem item)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (item.resourceType != null)
            {
                return resourcesService.CanClaim(item.resourceType);
            }

            return true;
        }
        
        private void Update()
        {
            timeLeftText.text = (message.ValidTime - DateTime.Now).TimerWithUnit() + " left";
        }
    }
}