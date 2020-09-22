using System;

namespace Backend.Models
{
    [Serializable]
    public class Achievements
    {
        public long playerId;
        public int simpleIncubationsDone;
        public int commonIncubationsDone;
        public int uncommonIncubationsDone;
        public int rareIncubationsDone;
        public int epicIncubationsDone;
        public int expeditionsDone;
        public int oddJobsDone;
        public int dailyRewardsClaimed;
        public long academyXpGained;
        public long academyAscGained;
        public long steamUsed;
        public long cogwheelsUsed;
        public long tokensUsed;
        public long coinsUsed;
        public long rubiesUsed;
        public long metalUsed;
        public long ironUsed;
        public long steelUsed;
        public long woodUsed;
        public long brownCoalUsed;
        public long blackCoalUsed;
        public int merchantItemsBought;
        public int mapTilesDiscovered;
        public long gearBreakdown;
        public int gearModified;
        public int jewelsMerged;
        public int buildingsUpgradesDone;
        public int vehiclesUpgradesDone;
        public int vehiclePartUpgradesDone;
        public int buildingMinLevel;
        public long woodenKeysCollected;
        public long bronzeKeysCollected;
        public long silverKeysCollected;
        public long goldenKeysCollected;
        public long chestsOpened;
    }
}