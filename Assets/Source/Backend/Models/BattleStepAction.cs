using System;

namespace Backend.Models
{
    [Serializable]
    public class BattleStepAction
    {
        public long id;
        public HeroPosition heroPosition;
        public string heroName;
        public BattleStepActionType type;
        public bool? crit;
        public bool? superCrit;
        public int? baseDamage;
        public string baseDamageText;
        public int? targetArmor;
        public int? targetHealth;
        public int? armorDiff;
        public int? healthDiff;
        public int? shieldAbsorb;
        public Buff buff;
        public int? buffIntensity;
        public int? buffDuration;
        public int? buffDurationChange;
        public int? speedbarFill;
    }
}