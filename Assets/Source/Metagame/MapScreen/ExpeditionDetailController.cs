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
    public class ExpeditionDetailController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text untilDoneTxt;
        [SerializeField] private TMP_Text description;
        [SerializeField] private RectTransform teamContainer;
        [SerializeField] private RectTransform lootContainer;
        [SerializeField] private HeroAvatarPrefabController heroAvatarPrefab;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatarPrefab;
        [SerializeField] private LootItemPrefabController lootItemPrefab;
        [SerializeField] private ButtonController actionButton;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;

        private PlayerExpedition expedition;
        private bool loading;
        private bool expeditionCancelled;

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
                expeditionService.FinishPlayerExpedition(expedition, data =>
                {
                    loading = false;
                    actionButton.ShowIndicator(false);
                    if (data.playerExpeditions?.IsEmpty() == false)
                    {
                        expedition = data.playerExpeditions[0];
                        UpdateExpedition();
                    }
                    else if (data.playerExpeditionCancelled != null)
                    {
                        expeditionCancelled = true;
                    }
                });
            });
        }

        private void ClosePopup()
        {
            var autobreakdown = new List<long>();
            if (expedition.lootedItems?.IsEmpty() == false)
            {
                expedition.lootedItems.ForEach(item =>
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
                loading = true;
                forgeService.Breakdown(autobreakdown, data =>
                {
                    loading = false;
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
            if (expeditionCancelled)
            {
                untilDoneTxt.text = "Aborted";
                actionButton.gameObject.SetActive(false);
            }
            else if (expedition.DoneTime <= DateTime.Now)
            {
                untilDoneTxt.text = "Completed";
                actionButton.SetText("Rewards");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonSuccess);
            }
            else
            {
                var timeLeft = expedition.DoneTime - DateTime.Now;
                untilDoneTxt.text = timeLeft.TimerWithUnit();
                actionButton.SetText("Abort");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonDanger);
            }
        }

        public void SetExpedition(PlayerExpedition exp)
        {
            expedition = exp;
            UpdateExpedition();
        }

        private void UpdateExpedition()
        {
            title.text = expedition.name;
            description.text = expedition.description;
            teamContainer.DetachChildren();
            var vehicle = vehicleService.Vehicle(expedition.vehicleId);
            if (vehicle != null)
            {
                var vehicleAvatar = Instantiate(vehicleAvatarPrefab, teamContainer);
                vehicleAvatar.SetVehicle(vehicle);
            }

            var hero1 = heroService.Hero(expedition.hero1Id);
            if (hero1 != null)
            {
                var hero1Avatar = Instantiate(heroAvatarPrefab, teamContainer);
                hero1Avatar.SetHero(hero1);
            }

            var hero2 = heroService.Hero(expedition.hero2Id);
            if (hero2 != null)
            {
                var hero2Avatar = Instantiate(heroAvatarPrefab, teamContainer);
                hero2Avatar.SetHero(hero2);
            }

            var hero3 = heroService.Hero(expedition.hero3Id);
            if (hero3 != null)
            {
                var hero3Avatar = Instantiate(heroAvatarPrefab, teamContainer);
                hero3Avatar.SetHero(hero3);
            }

            var hero4 = heroService.Hero(expedition.hero4Id);
            if (hero4 != null)
            {
                var hero4Avatar = Instantiate(heroAvatarPrefab, teamContainer);
                hero4Avatar.SetHero(hero4);
            }

            if (expedition.lootedItems?.IsEmpty() == false)
            {
                lootContainer.gameObject.SetActive(true);
                actionButton.gameObject.SetActive(false);
                expedition.lootedItems.ForEach(item =>
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
            else
            {
                lootContainer.gameObject.SetActive(false);
            }
        }
    }
}