using Backend.Models;

namespace Backend.Responses
{
    public struct PlayerActionResponse
    {
        public Player player;
        public Progress progress;
        public Achievements achievements;
        public string token;
        public Resources resources;
        public Hero hero;
        public Hero[] heroes;
        public long[] heroIdsRemoved;
        public Gear gear;
        public Gear[] gears;
        public long[] gearIdsRemovedFromArmory;
        public Jewelry[] jewelries;
        public Building[] buildings;
        public Vehicle[] vehicles;
        public VehiclePart[] vehicleParts;
        public PlayerMap[] playerMaps;
        public PlayerMap currentMap;
        public Battle ongoingBattle;
        public Looted looted;
        public Mission[] missions;
        public long? missionIdFinished;
        public Upgrade[] upgrades;
        public long? upgradeRemoved;
        public Incubator[] incubators;
        public long? incubatorDone;
        public string[] knownStories;
        public Expedition[] expeditions;
        public PlayerExpedition[] playerExpeditions;
        public long? playerExpeditionCancelled;
        public OddJob[] oddJobs;
        public long? oddJobDone;
        public DailyActivity dailyActivity;
        public MerchantPlayerItem[] merchantItems;
        public MerchantPlayerItem boughtMerchantItem;
        public BlackMarketItem[] blackMarketItems;
        public AchievementReward[] achievementRewards;
        public long? claimedAchievementRewardId;
        public AutoBreakdownConfiguration autoBreakdownConfiguration;
        public InboxMessage[] inboxMessages;
        public long? inboxMessageDeleted;
    }
}