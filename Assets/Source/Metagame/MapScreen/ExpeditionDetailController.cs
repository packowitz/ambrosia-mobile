using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
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
        [SerializeField] private RectTransform lootContainer;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatar;
        [SerializeField] private HeroAvatarPrefabController hero1;
        [SerializeField] private HeroAvatarPrefabController hero2;
        [SerializeField] private HeroAvatarPrefabController hero3;
        [SerializeField] private HeroAvatarPrefabController hero4;
        [SerializeField] private LootItemPrefabController lootItemPrefab;
        [SerializeField] private ButtonController actionButton;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;
        [Inject] private SignalBus signalBus;

        private PlayerExpedition expedition;
        private bool loading;
        private bool expeditionCancelled;

        private readonly List<LootItemPrefabController> lootItems = new List<LootItemPrefabController>();

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
                    if (data.playerExpeditionCancelled == expedition.id)
                    {
                        expeditionCancelled = true;
                    }
                });
            });
            signalBus.Subscribe<ExpeditionSignal>(ConsumeExpeditionSignal);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<ExpeditionSignal>(ConsumeExpeditionSignal);
        }

        private void ConsumeExpeditionSignal(ExpeditionSignal signal)
        {
            if (signal.Data != null && signal.Data.id == expedition?.id)
            {
                expedition = signal.Data;
                UpdateExpedition();
            }
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
            vehicleAvatar.SetVehicle(vehicleService.Vehicle(expedition.vehicleId));
            hero1.SetHero(heroService.Hero(expedition.hero1Id));
            hero2.SetHero(heroService.Hero(expedition.hero2Id));
            hero3.SetHero(heroService.Hero(expedition.hero3Id));
            hero4.SetHero(heroService.Hero(expedition.hero4Id));

            lootItems.ForEach(item => Destroy(item.gameObject));
            lootItems.Clear();
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
                    lootItems.Add(prefab);
                });
            }
            else
            {
                lootContainer.gameObject.SetActive(false);
            }
        }
    }
}