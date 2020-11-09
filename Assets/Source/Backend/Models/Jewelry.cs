using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Jewelry
    {
        public long id;
        public JewelType type;
        public GearJewelSlot slot;
        public int lvl1;
        public int lvl2;
        public int lvl3;
        public int lvl4;
        public int lvl5;
        public int lvl6;
        public int lvl7;
        public int lvl8;
        public int lvl9;
        public int lvl10;

        public Jewelry(JewelType type) {
            this.type = type;
        }

        // transient
        public bool expanded;
    }
}