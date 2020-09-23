using System;

namespace Backend.Models
{
    [Serializable]
    public class HeroSkillAction
    {
        public long id;
        public int position;
        public SkillActionTrigger trigger;
        public string triggerValue;
        public int triggerChance;
        public SkillActionType type;
        public SkillActionTarget target;
        public SkillActionEffect effect;
        public int effectValue;
        public int? effectDuration;
    }
}