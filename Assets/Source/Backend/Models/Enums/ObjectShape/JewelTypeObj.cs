using System;

namespace Backend.Models.ObjectShape
{
    [Serializable]
    public class JewelTypeObj
    {
        public string name;
        public GearJewelSlot slot;
        public GearSet gearSet;
    }
}