using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class BazaarService
    {
        public List<MerchantPlayerItem> MerchantItems { get; private set; }
        public List<BlackMarketItem> BlackMarketItems { get; private set; }

        public BazaarService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.merchantItems != null)
            {
                if (MerchantItems == null)
                {
                    MerchantItems = data.merchantItems;
                }
                else
                {
                    foreach (var merchantItem in data.merchantItems)
                    {
                        Update(merchantItem);
                    }
                }
            }

            if (data.boughtMerchantItem != null)
            {
                var item = MerchantItems.Find(m => m.id == data.boughtMerchantItem.id);
                if (item != null)
                {
                    item.sold = true;
                }
            }

            if (data.blackMarketItems != null)
            {
                BlackMarketItems = data.blackMarketItems;
            }
        }

        private void Update(MerchantPlayerItem merchantItem)
        {
            var idx = MerchantItems.FindIndex(m => m.id == merchantItem.id);
            if (idx >= 0)
            {
                MerchantItems[idx] = merchantItem;
            }
            else
            {
                MerchantItems.Add(merchantItem);
            }
        }
    }
}