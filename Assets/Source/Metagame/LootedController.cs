using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame
{
    public class LootedController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private RectTransform lootContainer;
        [SerializeField] private LootItemPrefabController lootItemPrefab;
        [SerializeField] private ButtonController actionButton;
        
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;
        [Inject] private ProgressService progressService;

        private Looted looted;

        private void Start()
        {
            dismiss.onClick.AddListener(() =>
            {
                ClosePopup();
            });
            actionButton.AddClickListener(() =>
            {
                ClosePopup();
            });
        }
        
        private void ClosePopup()
        {
            var autobreakdown = new List<long>();
            if (looted.items.IsEmpty() == false)
            {
                looted.items.ForEach(item =>
                {
                    if (item.type == LootedItemType.GEAR)
                    {
                        var gear = gearService.Gear(item.value);
                        if (gear?.markedToBreakdown == true)
                        {
                            autobreakdown.Add(gear.id);
                        }
                    }
                });
            }

            if (!autobreakdown.IsEmpty())
            {
                actionButton.ShowIndicator();
                forgeService.Breakdown(autobreakdown, data =>
                {
                    Destroy(gameObject);
                });
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetLooted(Looted lootedObj)
        {
            looted = lootedObj;
            switch (looted.type)
            {
                case LootedType.CHEST:
                    title.text = "Chest opened";
                    break;
                case LootedType.BATTLE:
                    title.text = "Battle rewards";
                    break;
                case LootedType.BREAKDOWN:
                    title.text = "Remnants collected";
                    break;
                case LootedType.STORY:
                    title.text = "Docs gift";
                    break;
                case LootedType.LEVEL_UP:
                    title.text = $"Rewards for reaching level {progressService.Progress.level}";
                    break;
                case LootedType.VIP_LEVEL_UP:
                    title.text = $"Rewards for reaching vip level {progressService.Progress.level}";
                    break;
                case LootedType.MERCHANT:
                case LootedType.BLACK_MARKET:
                    title.text = "You bought";
                    break;
                case LootedType.UPGRADE:
                    title.text = "Upgrade finished";
                    break;
                case LootedType.ODD_JOB:
                    title.text = "Odd job rewards";
                    break;
                case LootedType.DAILY_ACTIVITY:
                    title.text = "Daily activity reward";
                    break;
                case LootedType.TASK:
                    title.text = "Task rewards";
                    break;
            }
            
            lootContainer.gameObject.SetActive(true);
            actionButton.gameObject.SetActive(false);
            looted.items.ForEach(item =>
            {
                if (item.type == LootedItemType.GEAR)
                {
                    var gear = gearService.Gear(item.value);
                    gear.markedToBreakdown = forgeService.IsAutoBreakdown(gear);
                }
                var prefab = Instantiate(lootItemPrefab, lootContainer);
                prefab.SetItem(item, lootContainer.rect.height);
            });
        }
    }
}