using System;

namespace Backend.Models
{
    [Serializable]
    public class AutoBreakdownConfiguration
    {
        public long playerId;
        public int simpleMinJewelSlots;
        public int simpleMinQuality;
        public int commonMinJewelSlots;
        public int commonMinQuality;
        public int uncommonMinJewelSlots;
        public int uncommonMinQuality;
        public int rareMinJewelSlots;
        public int rareMinQuality;
        public int epicMinJewelSlots;
        public int epicMinQuality;
        public int legendaryMinJewelSlots;
        public int legendaryMinQuality;
    }
}