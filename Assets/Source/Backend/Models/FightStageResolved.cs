using System;

namespace Backend.Models
{
    [Serializable]
    public class FightStageResolved
    {
        public long id;
        public int stage;
        public Hero hero1;
        public Hero hero2;
        public Hero hero3;
        public Hero hero4;
    }
}