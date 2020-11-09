using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    public class BattleStep
    {
        public long id;
        public int turn;
        public BattleStepPhase phase;
        public HeroPosition actingHero;
        public string actingHeroName;
        public int? usedSkill;
        public string usedSkillName;
        public HeroPosition target;
        public string targetName;
        public List<BattleStepAction> actions;
        public List<BattleStepHeroState> heroStates;

        // transient
        public bool expanded;
    }
}