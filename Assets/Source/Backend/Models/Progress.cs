using System;

namespace Backend.Models
{
    [Serializable]
    public class Progress
    {
        public long playerId;
        public int xp;
        public int maxXp;
        public int level;
        public int vipPoints;
        public int vipLevel;
        public int vipMaxPoints;
        public long currentMapId;
        public int expeditionLevel;
        public int expeditionSpeed;
        public int numberOddJobs;
        public int garageSlots;
        public int offlineBattleSpeed;
        public int maxOfflineBattlesPerMission;
        public int builderQueueLength;
        public int builderSpeed;
        public int barrackSize;
        public int gearQualityIncrease;
        public int maxTrainingLevel;
        public int trainingXpBoost;
        public int trainingAscBoost;
        public int battleXpBoost;
        public int vehicleUpgradeLevel;
        public int incubators;
        public int labSpeed;
        public int simpleGenomesNeeded;
        public int commonGenomesNeeded;
        public int uncommonGenomesNeeded;
        public int rareGenomesNeeded;
        public int epicGenomesNeeded;
        public int simpleIncubationUpPerMil;
        public int commonIncubationUpPerMil;
        public int uncommonIncubationUpPerMil;
        public int uncommonIncubationSuperUpPerMil;
        public int rareIncubationUpPerMil;
        public int uncommonStartingLevel;
        public int maxJewelUpgradingLevel;
        public int jewelMergeDoubleChance;
        public int gearModificationRarity;
        public int gearModificationSpeed;
        public int gearBreakDownRarity;
        public int gearBreakDownResourcesInc;
        public bool autoBreakDownEnabled;
        public bool reRollGearQualityEnabled;
        public bool reRollGearStatEnabled;
        public bool incGearRarityEnabled;
        public bool reRollGearJewelEnabled;
        public bool addGearJewelEnabled;
        public bool addGearSpecialJewelEnabled;
        public int negotiationLevel;
        public bool tradingEnabled;
        public bool blackMarketEnabled;
        public bool carYardEnabled;
        public int merchantLevel;
    }
}