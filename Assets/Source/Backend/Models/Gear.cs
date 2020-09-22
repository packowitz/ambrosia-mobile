using System;

namespace Backend.Models
{
    [Serializable]
    public class Gear
    {
        public long id;
        public long playerId;
        public long equippedTo;
        public bool modificationInProgress;
        public bool modificationPerformed;
        public Modification modificationAllowed;
        public GearSet set;
        public Rarity rarity;
        public GearType type;
        public HeroStat stat;
        public int statValue;
        public int statQuality;
        public GearQuality gearQuality;
        public GearJewelSlot jewelSlot1;
        public JewelType jewel1Type;
        public int jewel1Level;
        public GearJewelSlot jewelSlot2;
        public JewelType jewel2Type;
        public int jewel2Level;
        public GearJewelSlot jewelSlot3;
        public JewelType jewel3Type;
        public int jewel3Level;
        public GearJewelSlot jewelSlot4;
        public JewelType jewel4Type;
        public int jewel4Level;
        public bool specialJewelSlot;
        public JewelType specialJewelType;
        public int specialJewelLevel;

        // transient
        public bool markedToBreakdown;
    }
}