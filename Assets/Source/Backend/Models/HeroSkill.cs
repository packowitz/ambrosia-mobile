using System;

namespace Backend.Models
{
    [Serializable]
    public class HeroSkill
    {
        public long id;
        public int number;
        public string name;
        public string icon;
        public bool passive;
        public PassiveSkillTrigger passiveSkillTrigger;
        public int? passiveSkillTriggerValue;
        public SkillActiveTrigger skillActiveTrigger;
        public int initCooldown;
        public int cooldown;
        public SkillTarget target;
        public string description;
        public int maxLevel;
        public HeroSkillLevel[] skillLevel;
        public HeroSkillAction[] actions;
    }
}