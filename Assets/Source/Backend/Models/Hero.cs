using System;

namespace Backend.Models
{
    [Serializable]
    public class Hero
    {
        public long id;
        public long? missionId;
        public long? playerExpeditionId;
        public long heroBaseId;
        public string name;
        public Color color;
        public string avatar;
        public int stars;
        public int level;
        public int xp;
        public int maxXp;
        public int skill1;
        public int? skill2;
        public int? skill3;
        public int? skill4;
        public int? skill5;
        public int? skill6;
        public int? skill7;
        public int skillPoints;
        public int ascLvl;
        public int ascPoints;
        public int ascPointsMax;
        public bool markedAsBoss;
        public Gear weapon;
        public Gear shield;
        public Gear helmet;
        public Gear armor;
        public Gear gloves;
        public Gear boots;
        public int baseStrength;
        public int baseHp;
        public int baseArmor;
        public int baseInitiative;
        public int baseCrit;
        public int baseCritMult;
        public int baseDexterity;
        public int baseResistance;

        public int strengthTotal;
        public int hpTotal;
        public int armorTotal;
        public int initiativeTotal;
        public int critTotal;
        public int critMultTotal;
        public int dexterityTotal;
        public int resistanceTotal;

        public HeroGearSet[] sets;
    }
}