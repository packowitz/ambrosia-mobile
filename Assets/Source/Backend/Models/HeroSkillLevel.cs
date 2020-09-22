using System;

namespace Backend.Models
{
    [Serializable]
    public class HeroSkillLevel
    {
        public long id;
        public int level;
        public string description;
    }
}