using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Task
    {
        public long id;
        public int number;
        public AchievementRewardType taskType;
        public long taskAmount;
        public long lootBoxId;

        // transient. only for player view
        public List<LootedItem> reward;
    }
}