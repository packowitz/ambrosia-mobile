using System;

namespace Backend.Models
{
    [Serializable]
    public class VehicleBase
    {
        public long id;
        public string name;
        public string avatar;
        public int maxLevel;
        public PartQuality engineQuality;
        public PartQuality frameQuality;
        public PartQuality computerQuality;
        public PartType specialPart1;
        public PartQuality specialPart1Quality;
        public PartType specialPart2;
        public PartQuality specialPart2Quality;
        public PartType specialPart3;
        public PartQuality specialPart3Quality;
    }
}