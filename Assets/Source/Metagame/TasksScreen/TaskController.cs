using System;
using Backend.Models;
using Backend.Models.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;
using Color = UnityEngine.Color;

namespace Metagame.TasksScreen
{
    public class TaskController : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image border;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text taskTitle;
        [SerializeField] private TMP_Text taskDescription;
        [SerializeField] private TMP_Text progressAmountText;
        [SerializeField] private RectTransform progressAmountTextCanvas;
        [SerializeField] private RectTransform progress;
        [SerializeField] private RectTransform progressBar;
        [SerializeField] private RectTransform rewardsCanvas;
        [SerializeField] private LootItemPrefabController lootItemPrefab;

        public Color neutralBackgroundColor;
        public Color claimableBackgroundColor;

        public void SetTask(TaskCluster taskCluster, Task task, long taskAmountDone)
        {
            taskTitle.text = $"{taskCluster.name} {NumberUtil.ToRoman(task.number)}";
            taskDescription.text = TaskDescription(task.taskType, task.taskAmount);
            task.reward.ForEach(item =>
            {
                var lootItem = Instantiate(lootItemPrefab, rewardsCanvas);
                lootItem.SetItem(item, 125f);
            });
            if (taskAmountDone >= task.taskAmount)
            {
                background.color = claimableBackgroundColor;
                border.gameObject.SetActive(true);
                progressAmountText.text = $"{NumberUtil.RoundAmount(task.taskAmount)}";

                progressBar.offsetMax = new Vector2(0, progressBar.offsetMax.y);
                var pos = progressAmountTextCanvas.localPosition;
                pos.x = progress.rect.width;
                progressAmountTextCanvas.localPosition = pos;
            }
            else
            {
                background.color = neutralBackgroundColor;
                border.gameObject.SetActive(false);
                progressAmountText.text = $"{NumberUtil.RoundAmount(taskAmountDone)}";

                var max = progress.rect.width;
                var donePercentage = Convert.ToSingle(taskAmountDone) / task.taskAmount;
                var right = max - max * donePercentage;
                progressBar.offsetMax = new Vector2(-right, progressBar.offsetMax.y);

                var pos = progressAmountTextCanvas.localPosition;
                pos.x = max * donePercentage;
                progressAmountTextCanvas.localPosition = pos;
            }
        }

        public void OnClaim(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        private string TaskDescription(AchievementRewardType type, long amount)
        {
            switch (type)
            {
                case AchievementRewardType.STEAM_USED: return $"Spend {amount} Steam";
                case AchievementRewardType.COGWHEELS_USED: return $"Spend {amount} Cogwheel{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.TOKENS_USED: return $"Spend {amount} Token{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.COINS_USED: return $"Spend {amount} Coins";
                case AchievementRewardType.RUBIES_USED: return $"Spend {amount} Rubies";
                case AchievementRewardType.METAL_USED: return $"Spend {amount} Metal";
                case AchievementRewardType.IRON_USED: return $"Spend {amount} Iron";
                case AchievementRewardType.STEEL_USED: return $"Spend {amount} Steel";
                case AchievementRewardType.WOOD_USED: return $"Spend {amount} Wood";
                case AchievementRewardType.BROWN_COAL_USED: return $"Spend {amount} Brown Coal";
                case AchievementRewardType.BLACK_COAL_USED: return $"Spend {amount} Black Coal";
                case AchievementRewardType.SIMPLE_INCUBATIONS: return $"Perform {amount} simple incubation{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.COMMON_INCUBATIONS: return $"Perform {amount} common incubation{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.UNCOMMON_INCUBATIONS: return $"Perform {amount} uncommon incubation{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.RARE_INCUBATIONS: return $"Perform {amount} rare incubation{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.EPIC_INCUBATIONS: return $"Perform {amount} epic incubation{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.EXPEDITIONS: return $"Finish {amount} expedition{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.ODD_JOBS: return $"Complete {amount} odd job{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.DAILY_ACTIVITY: return $"Claim {amount} daily activity rewards";
                case AchievementRewardType.ACADEMY_XP: return $"Gain {amount} XP in the academy";
                case AchievementRewardType.ACADEMY_ASC: return $"Gain {amount} ASC points in the academy";
                case AchievementRewardType.MERCHANT_ITEMS_BOUGHT: return $"Purchase {amount} item{(amount > 1 ? "s" : "")} from the merchant";
                case AchievementRewardType.MAP_TILES_DISCOVERED: return $"Discover {amount} map tile{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.GEAR_MODIFICATIONS: return $"Perform {amount} gear modification{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.GEAR_BREAKDOWN: return $"Break down {amount} gear item{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.JEWELS_MERGED: return $"Merge {amount} jewel{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.BUILDING_UPGRADES: return $"Perform {amount} building upgrade{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.VEHICLE_UPGRADES: return $"Perform {amount} vehicle upgrade{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.VEHICLE_PART_UPGRADES: return $"Perform {amount} vehicle part upgrade{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.BUILDING_MIN_LEVEL: return $"Have all buildings upgraded to level {amount}";
                case AchievementRewardType.WOODEN_KEYS_COLLECTED: return $"Collect {amount} wooden key{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.BRONZE_KEYS_COLLECTED: return $"Collect {amount} bronze key{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.SILVER_KEYS_COLLECTED: return $"Collect {amount} silver key{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.GOLDEN_KEYS_COLLECTED: return $"Collect {amount} golden key{(amount > 1 ? "s" : "")}";
                case AchievementRewardType.CHESTS_OPENED: return $"Open {amount} chest{(amount > 1 ? "s" : "")}";
                default: return $"Unknown task type {type}";
            }
        }
    }
}