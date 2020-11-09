using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class BattleStepHeroState
    {
        public long id;
        public HeroPosition position;
        public HeroStatus status;
        public int hpPerc;
        public int armorPerc;
        public int speedbarPerc;

        public List<BattleStepHeroStateBuff> buffs;
    }
}