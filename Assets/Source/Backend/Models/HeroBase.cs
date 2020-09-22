using System;

namespace Backend.Models
{
    [Serializable]
    public class HeroBase
    {
        public long id;
        public string name;
        public Rarity rarity;
        public string heroClass;
        public Color color;
        public bool startingHero;
        public HeroType heroType;
        public string avatar;
        public int strengthBase;
        public int strengthFull;
        public int hpBase;
        public int hpFull;
        public int armorBase;
        public int armorFull;
        public int initiative;
        public int initiativeAsc;
        public int crit;
        public int critAsc;
        public int critMult;
        public int critMultAsc;
        public int dexterity;
        public int dexterityAsc;
        public int resistance;
        public int resistanceAsc;
        public bool recruitable;
        public int maxAscLevel;
        public HeroSkill[] skills;
    }
}