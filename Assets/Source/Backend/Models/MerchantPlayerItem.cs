using System;
using Backend.Models.ObjectShape;

namespace Backend.Models
{
    [Serializable]
    public class MerchantPlayerItem
    {
        public long id;
        public long playerId;
        public int sortOrder;
        public int merchantLevel;
        public bool sold;
        public ResourceType priceType;
        public int priceAmount;
        public ResourceType resourceType;
        public int? resourceAmount;
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