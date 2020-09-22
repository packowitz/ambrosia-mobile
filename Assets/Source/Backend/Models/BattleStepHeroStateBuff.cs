using System;

namespace Backend.Models
{
    [Serializable]
    public class BattleStepHeroStateBuff
    {
        public long id;
        public Buff buff;
        public BuffType type;
        public int intensity;
        public int duration;
    }
}