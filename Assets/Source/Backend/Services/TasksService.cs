using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Responses;
using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Backend.Services
{
    public class TasksService
    {
        private readonly ServerAPI serverAPI;
        private SignalBus signalBus;
        
        private Achievements achievements;
        public List<PlayerTask> PlayerTasks { get; private set; }
        public List<TaskCluster> TaskClusters { get; private set; }
        
        public bool TaskClustersInitialized => TaskClusters != null && TaskClusters.Count > 0;

        public TasksService(SignalBus signalBus, ServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public void ClaimTask(TaskCluster cluster, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/tasks/claim/{cluster.id}", null, onSuccess);
        }

        public bool HasClaimableTask()
        {
            return PlayerTasks.Exists(playerTask =>
            {
                var taskCluster = TaskClusters.Find(cluster => playerTask.taskClusterId == cluster.id);
                var task = taskCluster?.tasks.Find(t => t.number == playerTask.currentTaskNumber);
                return task != null && AchievementAmount(task.taskType) >= task.taskAmount;
            });
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

            if (data.playerTasks != null)
            {
                if (PlayerTasks == null)
                {
                    PlayerTasks = data.playerTasks;
                }
                else
                {
                    foreach (var playerTask in data.playerTasks)
                    {
                        Update(playerTask);
                    }
                }
            }

            if (data.achievements != null || data.playerTasks != null)
            {
                signalBus.Fire<TaskSignal>();
            }
        }

        private void Update(PlayerTask playerTask)
        {
            var idx = PlayerTasks.FindIndex(a => a.id == playerTask.id);
            if (idx >= 0)
            {
                PlayerTasks[idx] = playerTask;
            }
            else
            {
                PlayerTasks.Add(playerTask);
            }
        }

        public void LoadTaskClusters()
        {
            serverAPI.DoGet<List<TaskCluster>>("/tasks", data =>
            {
                TaskClusters = data;
                Debug.Log($"Loaded {TaskClusters.Count} task clusters");
            });
        }
    }
}