using System;
using Backend.Models;
using Backend.Models.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace Metagame.TasksScreen
{
    public class OddJobController : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image border;
        [SerializeField] private Button button;
        [SerializeField] private Button deleteButton;
        [SerializeField] private TMP_Text jobText;
        [SerializeField] private TMP_Text progressAmountText;
        [SerializeField] private RectTransform progressAmountTextCanvas;
        [SerializeField] private RectTransform progress;
        [SerializeField] private RectTransform progressBar;
        [SerializeField] private RectTransform rewardsCanvas;
        [SerializeField] private LootItemPrefabController lootItemPrefab;
        [SerializeField] private TMP_Text noJobText;

        public Color neutralBackgroundColor;
        public Color claimableBackgroundColor;

        public void SetJob(OddJob job)
        {
            if (job == null)
            {
                noJobText.gameObject.SetActive(true);
                jobText.gameObject.SetActive(false);
                deleteButton.gameObject.SetActive(false);
                progress.gameObject.SetActive(false);
                rewardsCanvas.gameObject.SetActive(false);
            }
            else
            {
                noJobText.gameObject.SetActive(false);
                jobText.text = JobDescription(job.jobType, job.jobAmount);
                job.reward.ForEach(item =>
                {
                    var lootItem = Instantiate(lootItemPrefab, rewardsCanvas);
                    lootItem.SetItem(item);
                });
                if (job.jobAmountDone >= job.jobAmount)
                {
                    background.color = claimableBackgroundColor;
                    border.gameObject.SetActive(true);
                    progressAmountText.text = $"{job.jobAmount}";
                    deleteButton.gameObject.SetActive(false);

                    progressBar.offsetMax = new Vector2(0, progressBar.offsetMax.y);
                    var pos = progressAmountTextCanvas.localPosition;
                    pos.x = progress.rect.width;
                    progressAmountTextCanvas.localPosition = pos;
                }
                else
                {
                    background.color = neutralBackgroundColor;
                    border.gameObject.SetActive(false);
                    progressAmountText.text = $"{job.jobAmountDone}";

                    var max = progress.rect.width;
                    var donePercentage = Convert.ToSingle(job.jobAmountDone) / job.jobAmount;
                    var right = max - max * donePercentage;
                    progressBar.offsetMax = new Vector2(-right, progressBar.offsetMax.y);

                    var pos = progressAmountTextCanvas.localPosition;
                    pos.x = max * donePercentage;
                    progressAmountTextCanvas.localPosition = pos;
                }
            }
        }

        public void OnDelete(UnityAction call)
        {
            deleteButton.onClick.AddListener(call);
        }

        public void OnClaim(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        private string JobDescription(OddJobType type, int amount)
        {
            switch (type)
            {
                case OddJobType.SPEND_STEAM: return $"Spend {amount} Steam for either battles or map discovery";
                case OddJobType.SPEND_COGWHEELS: return $"Spend {amount} Cogwheels for battles in dungeons";
                case OddJobType.SPEND_TOKENS: return $"Spend {amount} Tokens in the arena";
                case OddJobType.FINISH_MISSIONS: return $"Finish {amount} mission battles";
                case OddJobType.OPEN_CHESTS: return $"Open {amount} chests anywhere on any map";
                case OddJobType.DISCOVER_TILES: return $"Discover {amount} tiles on any map";
                case OddJobType.UPGRADE_BUILDING: return $"Finish {amount} building upgrade{(amount > 1 ? "s" : "")}";
                case OddJobType.UPGRADE_VEHICLE: return $"Finish {amount} vehicle upgrade{(amount > 1 ? "s" : "")}";
                case OddJobType.UPGRADE_PARTS: return $"Finish {amount} vehicle part upgrade{(amount > 1 ? "s" : "")}";
                case OddJobType.MERGE_JEWELS: return $"Merge {amount} jewel{(amount > 1 ? "s" : "")} to a higher one";
                case OddJobType.MODIFY_GEAR: return $"Perform {amount} gear modification{(amount > 1 ? "s" : "")}";
                case OddJobType.BREAKDOWN_GEAR: return $"Break down {amount} gear item{(amount > 1 ? "s" : "")}";
                case OddJobType.FINISH_EXPEDITIONS: return $"Finish {amount} expedition{(amount > 1 ? "s" : "")}";
                case OddJobType.LOOT_GEAR:
                    return $"Loot {amount} gear item{(amount > 1 ? "s" : "")} from battles or chests";
                case OddJobType.LOOT_PARTS:
                    return $"Loot {amount} vehicle part{(amount > 1 ? "s" : "")} from battles or chests";
                case OddJobType.LOOT_COINS:
                    return $"Loot {amount} coin{(amount > 1 ? "s" : "")} from battles or chests";
                case OddJobType.LOOT_JEWELS:
                    return $"Loot {amount} jewels{(amount > 1 ? "s" : "")} from battles or chests";
                default: return $"Unknown job {type}";
            }
        }
    }
}