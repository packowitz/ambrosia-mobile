using System;
using Backend.Models.Enums;
using Backend.Models.Enums.ObjectShape;

namespace Backend.Models
{
    [Serializable]
    public class LootedItem
    {
        public LootedItemType type;
        public ResourceType resourceType;
        public ProgressStat progressStat;
        public JewelTypeObj jewelType;
        public long value;
    }
}