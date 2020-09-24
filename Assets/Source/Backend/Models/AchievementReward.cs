using System;

namespace Backend.Models
{
    [Serializable]
    public class AchievementReward
    {
        public long id;
        public bool starter;
        public string name;
        public AchievementRewardType achievementType;
        public long achievementAmount;
        public long? followUpReward;
        public long lootBoxId;

        // transient. only for player view
        public LootedItem[] reward;
    }
}