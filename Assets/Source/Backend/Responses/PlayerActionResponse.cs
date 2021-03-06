using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;

// ReSharper disable UnassignedField.Global
// ReSharper disable InconsistentNaming

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
        public List<Hero> heroes;
        public List<long> heroIdsRemoved;
        public Gear gear;
        public List<Gear> gears;
        public List<long> gearIdsRemovedFromArmory;
        public List<Jewelry> jewelries;
        public List<Building> buildings;
        public List<Vehicle> vehicles;
        public List<VehiclePart> vehicleParts;
        public List<PlayerMap> playerMaps;
        public PlayerMap currentMap;
        public Battle ongoingBattle;
        public Looted looted;
        public List<Mission> missions;
        public long? missionIdFinished;
        public List<Upgrade> upgrades;
        public long? upgradeRemoved;
        public List<Incubator> incubators;
        public long? incubatorDone;
        public List<StoryTrigger> knownStories;
        public List<Expedition> expeditions;
        public List<PlayerExpedition> playerExpeditions;
        public long? playerExpeditionCancelled;
        public List<OddJob> oddJobs;
        public long? oddJobDone;
        public DailyActivity dailyActivity;
        public List<MerchantPlayerItem> merchantItems;
        public MerchantPlayerItem boughtMerchantItem;
        public List<BlackMarketItem> blackMarketItems;
        public AutoBreakdownConfiguration autoBreakdownConfiguration;
        public List<InboxMessage> inboxMessages;
        public long? inboxMessageDeleted;
        public List<Team> teams;
        public List<PlayerTask> playerTasks;
    }
}