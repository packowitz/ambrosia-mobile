using System;
using System.Collections.Generic;

namespace Backend.Models
{
    [Serializable]
    public class LootItem
    {
        public long id;
        public int slotNumber;
        public int itemOrder;
        public int chance;
        public Color color;
        public LootItemType type;
        public ResourceType resourceType;
        public int? resourceFrom;
        public int? resourceTo;
        public long? heroBaseId;
        public int? heroLevel;
        public long? gearLootId;
        public List<JewelType> jewelTypes;
        public int? jewelLevel;
        public long? vehicleBaseId;
        public PartType vehiclePartType;
        public PartQuality vehiclePartQuality;
        public ProgressStat progressStat;
        public int? progressStatBonus;
    }
}