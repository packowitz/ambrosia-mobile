using System;

namespace Backend.Models
{
    [Serializable]
    public class ExpeditionBase
    {
        public long id;
        public string name;
        public string description;
        public int level;
        public Rarity rarity;
        public int durationMinutes;
        public int xp;
        public long lootBoxId;
    }
}