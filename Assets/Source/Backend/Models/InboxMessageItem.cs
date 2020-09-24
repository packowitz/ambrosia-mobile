using System;
using Backend.Models.ObjectShape;

namespace Backend.Models
{
    [Serializable]
    public class InboxMessageItem
    {
        public long id;
        public int number;
        public LootItemType type;
        public ResourceType resourceType;
        public int? resourceAmount;
        public long? heroBaseId;
        public int? heroLevel;
        public JewelTypeObj jewelType;
        public int? jewelLevel;
        public long? vehicleBaseId;
        public PartType vehiclePartType;
        public PartQuality vehiclePartQuality;
        public ProgressStat progressStat;
        public int? progressBonus;
    }
}