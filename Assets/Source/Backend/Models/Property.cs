using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Property
    {
        public long id;
        public PropertyCategory category;
        public PropertyType type;
        public int? level;
        public HeroStat stat;
        public ProgressStat progressStat;
        public ResourceType resourceType;
        public VehicleStat vehicleStat;
        public int value1;
        public int? value2;
    }
}