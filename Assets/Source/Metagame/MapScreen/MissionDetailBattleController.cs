using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Configs;
using ModestTree;
using TMPro;
using UnityEngine;
using Zenject;

namespace Metagame.MapScreen
{
    public class MissionDetailBattleController : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private RectTransform progress;
        [SerializeField] private RectTransform progressBar;
        [SerializeField] private RectTransform rewardsCanvas;
        [SerializeField] private LootItemPrefabController lootItemPrefab;

        [Inject] private ConfigsProvider configs;
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;

        private OfflineBattle battle;
        private readonly List<LootItemPrefabController> lootItems = new List<LootItemPrefabController>();

        public void SetBattle(OfflineBattle offlineBattle, int battleNumber)
        {
            var colorConfigs = configs.Get<ColorsConfig>();
            battle = offlineBattle;
            title.text = $"{battleNumber}. Battle";
            
            progress.gameObject.SetActive(!battle.cancelled && battle.battleStarted && !battle.battleFinished);
            rewardsCanvas.gameObject.SetActive(battle.battleSuccess && battle.lootedItems?.IsEmpty() == false);
            if (battle.cancelled)
            {
                resultText.text = "aborted";
                resultText.color = colorConfigs.missionIdle;
                resultText.gameObject.SetActive(true);
            }
            else if (battle.battleFinished)
            {
                if (battle.battleSuccess)
                {
                    if (battle.lootedItems?.IsEmpty() == false)
                    {
                        resultText.gameObject.SetActive(false);
                        lootItems.ForEach(lootItem => Destroy(lootItem.gameObject));
                        lootItems.Clear();
                        battle.lootedItems.ForEach(item =>
                        {
                            if (item.type == LootedItemType.GEAR)
                            {
                                var gear = gearService.Gear(item.value);
                                gear.markedToBreakdown = forgeService.IsAutoBreakdown(gear);
                            }
                            var prefab = Instantiate(lootItemPrefab, rewardsCanvas);
                            prefab.SetItem(item, rewardsCanvas.rect.height);
                            lootItems.Add(prefab);
                        });
                    }
                    else
                    {
                        resultText.text = "success";
                        resultText.color = colorConfigs.missionSuccess;
                        resultText.gameObject.SetActive(true);
                    }
                }
                else
                {
                    resultText.text = "failed";
                    resultText.color = colorConfigs.missionFailed;
                    resultText.gameObject.SetActive(true);
                }
            }
            else if (battle.battleStarted)
            {
                resultText.gameObject.SetActive(false);
            }
            else
            {
                resultText.text = "starts soon";
                resultText.color = colorConfigs.missionIdle;
                resultText.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (!battle.cancelled && battle.battleStarted && !battle.battleFinished)
            {
                var timeLeft = Convert.ToSingle((battle.DoneTime - DateTime.Now).TotalSeconds);
                if (timeLeft <= 0)
                {
                    progressBar.offsetMax = new Vector2(0, progressBar.offsetMax.y);
                }
                else
                {
                    var max = progress.rect.width;
                    var donePercentage = (battle.duration - timeLeft) / battle.duration;
                    var right = max - max * donePercentage;
                    progressBar.offsetMax = new Vector2(-right, progressBar.offsetMax.y);
                }
                
            }
        }
    }
}