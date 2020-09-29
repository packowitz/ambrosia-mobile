using System;
using System.Collections.Generic;

namespace Backend.Models
{
    [Serializable]
    public class BattleHero
    {
        public long id;
        public long? playerId;
        public long heroId;
        public HeroStatus status;
        public HeroBase heroBase;
        public HeroPosition position;
        public Color color;
        public int level;
        public int stars;
        public int ascLvl;
        public bool markedAsBoss;
 
        public List<BattleHeroBuff> buffs;
 
        public int skill1Lvl;
        public int? skill2Lvl;
        public int? skill2Cooldown;
        public int? skill3Lvl;
        public int? skill3Cooldown;
        public int? skill4Lvl;
        public int? skill4Cooldown;
        public int? skill5Lvl;
        public int? skill5Cooldown;
        public int? skill6Lvl;
        public int? skill6Cooldown;
        public int? skill7Lvl;
        public int? skill7Cooldown;
 
        public int heroStrength;
        public int strengthBonus;
        public int heroHp;
        public int heroArmor;
        public int armorBonus;
        public int heroCrit;
        public int critBonus;
        public int heroCritMult;
        public int critMultBonus;
        public int heroDexterity;
        public int dexterityBonus;
        public int heroResistance;
        public int resistanceBonus;
 
        public int currentHp;
        public int currentArmor;
        public int currentSpeedBar;

        public int heroLifesteal;
        public int lifestealBonus;
        public int heroCounterChance;
        public int counterChanceBonus;
        public int heroReflect;
        public int reflectBonus;
        public int heroDodgeChance;
        public int dodgeChanceBonus;
        public int heroSpeed;
        public int speedBonus;
        public int heroArmorPiercing;
        public int armorPiercingBonus;
        public int heroArmorExtraDmg;
        public int armorExtraDmgBonus;
        public int heroHealthExtraDmg;
        public int healthExtraDmgBonus;
        public int heroRedDamageInc;
        public int redDamageIncBonus;
        public int heroGreenDamageInc;
        public int greenDamageIncBonus;
        public int heroBlueDamageInc;
        public int blueDamageIncBonus;
        public int heroHealingInc;
        public int healingIncBonus;
        public int heroSuperCritChance;
        public int superCritChanceBonus;
        public int heroBuffIntensityInc;
        public int buffIntensityIncBonus;
        public int heroDebuffIntensityInc;
        public int debuffIntensityIncBonus;
    }
}