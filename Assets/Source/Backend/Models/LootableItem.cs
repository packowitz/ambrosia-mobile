using System;
using Backend.Models.Enums;
using Backend.Models.Enums.ObjectShape;

namespace Backend.Models
{
    [Serializable]
    public class LootableItem
    {
        public LootItemType type;
        public ResourceType resourceType;
        public int? resourceAmount;
        public ProgressStat progressStat;
        public int? progressBonus;
        public long? heroBaseId;
        public int? heroLevel;
        public Gear gear;
        public JewelTypeObj jewelType;
        public int? jewelLevel;
        public long? vehicleBaseId;
        public PartType vehiclePartType;
        public PartQuality vehiclePartQuality;
        public int secondsUntilRefresh;
    }
}