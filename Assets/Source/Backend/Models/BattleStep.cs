namespace Backend.Models
{
    public class BattleStep
    {
        public long id;
        public int turn;
        public BattleStepPhase phase;
        public HeroPosition actingHero;
        public string actingHeroName;
        public int usedSkill;
        public string usedSkillName;
        public HeroPosition target;
        public string targetName;
        public BattleStepAction[] actions;
        public BattleStepHeroState[] heroStates;

        // transient
        public bool expanded;
    }
}