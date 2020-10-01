using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class AchievementsService
    {
        private Achievements achievements;
        public List<AchievementReward> AchievementRewards { get; private set; }

        public AchievementsService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public bool HasClaimableReward()
        {
            return AchievementRewards.FindIndex(a => AchievementAmount(a.achievementType) >= a.achievementAmount) >= 0;
        }

        public long AchievementAmount(AchievementRewardType type)
        {
            switch (type)
            {
                case AchievementRewardType.STEAM_USED:
                    return achievements.steamUsed;
                case AchievementRewardType.COGWHEELS_USED:
                    return achievements.cogwheelsUsed;
                case AchievementRewardType.TOKENS_USED:
                    return achievements.tokensUsed;
                case AchievementRewardType.COINS_USED:
                    return achievements.coinsUsed;
                case AchievementRewardType.RUBIES_USED:
                    return achievements.rubiesUsed;
                case AchievementRewardType.METAL_USED:
                    return achievements.metalUsed;
                case AchievementRewardType.IRON_USED:
                    return achievements.ironUsed;
                case AchievementRewardType.STEEL_USED:
                    return achievements.steelUsed;
                case AchievementRewardType.WOOD_USED:
                    return achievements.woodUsed;
                case AchievementRewardType.BROWN_COAL_USED:
                    return achievements.brownCoalUsed;
                case AchievementRewardType.BLACK_COAL_USED:
                    return achievements.blackCoalUsed;
                case AchievementRewardType.SIMPLE_INCUBATIONS:
                    return achievements.simpleIncubationsDone;
                case AchievementRewardType.COMMON_INCUBATIONS:
                    return achievements.commonIncubationsDone;
                case AchievementRewardType.UNCOMMON_INCUBATIONS:
                    return achievements.uncommonIncubationsDone;
                case AchievementRewardType.RARE_INCUBATIONS:
                    return achievements.rareIncubationsDone;
                case AchievementRewardType.EPIC_INCUBATIONS:
                    return achievements.epicIncubationsDone;
                case AchievementRewardType.EXPEDITIONS:
                    return achievements.expeditionsDone;
                case AchievementRewardType.ODD_JOBS:
                    return achievements.oddJobsDone;
                case AchievementRewardType.DAILY_ACTIVITY:
                    return achievements.dailyRewardsClaimed;
                case AchievementRewardType.ACADEMY_XP:
                    return achievements.academyXpGained;
                case AchievementRewardType.ACADEMY_ASC:
                    return achievements.academyAscGained;
                case AchievementRewardType.MERCHANT_ITEMS_BOUGHT:
                    return achievements.merchantItemsBought;
                case AchievementRewardType.MAP_TILES_DISCOVERED:
                    return achievements.mapTilesDiscovered;
                case AchievementRewardType.GEAR_MODIFICATIONS:
                    return achievements.gearModified;
                case AchievementRewardType.GEAR_BREAKDOWN:
                    return achievements.gearBreakdown;
                case AchievementRewardType.JEWELS_MERGED:
                    return achievements.jewelsMerged;
                case AchievementRewardType.BUILDING_UPGRADES:
                    return achievements.buildingsUpgradesDone;
                case AchievementRewardType.VEHICLE_UPGRADES:
                    return achievements.vehiclesUpgradesDone;
                case AchievementRewardType.VEHICLE_PART_UPGRADES:
                    return achievements.vehiclePartUpgradesDone;
                case AchievementRewardType.BUILDING_MIN_LEVEL:
                    return achievements.buildingMinLevel;
                case AchievementRewardType.WOODEN_KEYS_COLLECTED:
                    return achievements.woodenKeysCollected;
                case AchievementRewardType.BRONZE_KEYS_COLLECTED:
                    return achievements.bronzeKeysCollected;
                case AchievementRewardType.SILVER_KEYS_COLLECTED:
                    return achievements.silverKeysCollected;
                case AchievementRewardType.GOLDEN_KEYS_COLLECTED:
                    return achievements.goldenKeysCollected;
                case AchievementRewardType.CHESTS_OPENED:
                    return achievements.chestsOpened;
                default: throw new Exception($"Unhandled AchievementRewardType: {type}");
            }
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.achievements != null)
            {
                achievements = data.achievements;
            }

            if (data.achievementRewards != null)
            {
                if (AchievementRewards == null)
                {
                    AchievementRewards = data.achievementRewards;
                    SortAchievementRewards();
                }
                else
                {
                    foreach (var dataAchievementReward in data.achievementRewards)
                    {
                        Update(dataAchievementReward);
                    }
                }
            }

            if (data.claimedAchievementRewardId != null)
            {
                var idx = AchievementRewards.FindIndex(a => a.id == data.claimedAchievementRewardId);
                if (idx >= 0)
                {
                    AchievementRewards.RemoveAt(idx);
                }
            }
        }

        private void Update(AchievementReward achievementReward)
        {
            var idx = AchievementRewards.FindIndex(a => a.id == achievementReward.id);
            if (idx >= 0)
            {
                AchievementRewards[idx] = achievementReward;
            }
            else
            {
                AchievementRewards.Add(achievementReward);
                SortAchievementRewards();
            }
        }

        private void SortAchievementRewards()
        {
            AchievementRewards = AchievementRewards.OrderBy(a => a.name).ToList();
        }
    }
}