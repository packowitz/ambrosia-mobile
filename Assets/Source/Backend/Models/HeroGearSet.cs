using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class HeroGearSet
    {
        public GearSet gearSet;
        public int number;
        public string description;
    }
}