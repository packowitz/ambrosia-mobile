using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class OddJob
    {
        public long id;
        public long playerId;
        public long oddJobBaseId;
        public int level;
        public string name;
        public Rarity rarity;
        public OddJobType jobType;
        public int jobAmount;
        public int jobAmountDone;
        public List<LootedItem> reward;
    }
}