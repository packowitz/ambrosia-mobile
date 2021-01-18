using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Configs;
using Metagame.HeroAvatar;
using Metagame.VehicleAvatar;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame.MapScreen
{
    public class MissionDetailController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text untilDoneTxt;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatar;
        [SerializeField] private HeroAvatarPrefabController hero1;
        [SerializeField] private HeroAvatarPrefabController hero2;
        [SerializeField] private HeroAvatarPrefabController hero3;
        [SerializeField] private HeroAvatarPrefabController hero4;
        [SerializeField] private LootItemPrefabController lootItemPrefab;
        [SerializeField] private ButtonController actionButton;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private MissionService missionService;
        [Inject] private MapService mapService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;

        private PlayerExpedition expedition;
        private Mission mission;
        private bool loading;

        private void Start()
        {
            dismiss.onClick.AddListener(() =>
            {
                if (!loading)
                {
                    ClosePopup();
                }
            });
            actionButton.AddClickListener(() =>
            {
                loading = true;
                actionButton.ShowIndicator();
                missionService.FinishMission(mission, data =>
                {
                    loading = false;
                    actionButton.ShowIndicator(false);
                    if (data.missions?.IsEmpty() == false)
                    {
                        mission = data.missions[0];
                        UpdateMission();
                    }
                });
            });
        }

        private void ClosePopup()
        {
            var autobreakdown = new List<long>();
            mission.battles.ForEach(battle =>
            {
                if (battle.lootedItems?.IsEmpty() == false)
                {
                    battle.lootedItems.ForEach(item =>
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
            });

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

        private void Update()
        {
            if (mission.lootCollected)
            {
                untilDoneTxt.text = "Aborted";
                actionButton.gameObject.SetActive(false);
            }
            else if (mission.DoneTime <= DateTime.Now)
            {
                untilDoneTxt.text = "Completed";
                actionButton.SetText("Finish");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonSuccess);
            }
            else
            {
                var timeLeft = mission.DoneTime - DateTime.Now;
                untilDoneTxt.text = timeLeft.TimerWithUnit();
                actionButton.SetText("Abort");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonDanger);
            }
        }

        public void SetMission(Mission playerMission)
        {
            mission = playerMission;
            UpdateMission();
        }

        private void UpdateMission()
        {
            title.text = $"{mapService.GetMapName(mission.mapId)} {mission.posX}x{mission.posY}";
            vehicleAvatar.SetVehicle(vehicleService.Vehicle(mission.vehicleId));
            hero1.SetHero(heroService.Hero(mission.hero1Id));
            hero2.SetHero(heroService.Hero(mission.hero2Id));
            hero3.SetHero(heroService.Hero(mission.hero3Id));
            hero4.SetHero(heroService.Hero(mission.hero4Id));

            // if (expedition.lootedItems?.IsEmpty() == false)
            // {
            //     lootContainer.gameObject.SetActive(true);
            //     actionButton.gameObject.SetActive(false);
            //     expedition.lootedItems.ForEach(item =>
            //     {
            //         if (item.type == LootedItemType.GEAR)
            //         {
            //             var gear = gearService.Gear(item.value);
            //             gear.markedToBreakdown = forgeService.IsAutoBreakdown(gear);
            //         }
            //         var prefab = Instantiate(lootItemPrefab, lootContainer);
            //         prefab.SetItem(item, lootContainer.rect.height);
            //     });
            // }
            // else
            // {
            //     lootContainer.gameObject.SetActive(false);
            // }
        }
    }
}