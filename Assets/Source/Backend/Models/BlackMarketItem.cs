using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class BlackMarketItem
    {
        public long id;
        public bool active;
        public int sortOrder;
        public long lootBoxId;
        public ResourceType priceType;
        public int priceAmount;

        public LootableItem lootableItem;
    }
}