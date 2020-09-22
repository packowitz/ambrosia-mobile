using System;

namespace Backend.Models
{
    [Serializable]
    public class FightStage
    {
        public long id;
        public int stage;
        public long hero1Id;
        public long hero2Id;
        public long hero3Id;
        public long hero4Id;
    }
}