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
        [SerializeField] private ButtonController closeButton;
        [SerializeField] private ButtonController actionButton;
        [SerializeField] private RectTransform progressBar;
        [SerializeField] private RectTransform battlesCanvas;
        [SerializeField] private RectTransform battlesContainer;
        [SerializeField] private MissionDetailBattleController missionBattlePrefab;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private MissionService missionService;
        [Inject] private MapService mapService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private GearService gearService;
        [Inject] private ForgeService forgeService;
        [Inject] private SignalBus signalBus;

        private PlayerExpedition expedition;
        private Mission mission;
        private bool loading;
        private const float BATTLE_HEIGHT = 135f;

        private readonly Dictionary<long, MissionDetailBattleController> battleViews =
            new Dictionary<long, MissionDetailBattleController>();

        private void Start()
        {
            dismiss.onClick.AddListener(() =>
            {
                if (!loading)
                {
                    ClosePopup();
                }
            });
            closeButton.AddClickListener(() =>
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
                    actionButton.gameObject.SetActive(false);
                });
            });
            signalBus.Subscribe<MissionSignal>(ConsumeMissionSignal);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<MissionSignal>(ConsumeMissionSignal);
        }

        private void ConsumeMissionSignal(MissionSignal signal)
        {
            if (signal.Data.id == mission?.id)
            {
                mission = signal.Data;
                UpdateMission();
            }
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
            if (mission.battles.Find(m => m.cancelled) != null)
            {
                untilDoneTxt.text = "Aborted";
                actionButton.gameObject.SetActive(false);
            }
            else if (mission.DoneTime <= DateTime.Now)
            {
                untilDoneTxt.text = "Completed";
                actionButton.SetText("Finish");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonSuccess);
                progressBar.offsetMax = new Vector2(0, progressBar.offsetMax.y);
            }
            else
            {
                var timeLeft = mission.DoneTime - DateTime.Now;
                untilDoneTxt.text = timeLeft.TimerWithUnit();
                actionButton.SetText("Abort");
                actionButton.SetColor(configsProvider.Get<ColorsConfig>().buttonDanger);
                var max = ((RectTransform) progressBar.parent).rect.width;
                var donePercentage = (mission.duration - Convert.ToSingle(timeLeft.TotalSeconds)) / mission.duration;
                var right = max - max * donePercentage;
                progressBar.offsetMax = new Vector2(-right, progressBar.offsetMax.y);
            }
        }

        public void SetMission(Mission playerMission)
        {
            mission = playerMission;
            var battlesHeight = BATTLE_HEIGHT * mission.totalCount;
            if (battlesHeight < battlesContainer.rect.height)
            {
                battlesContainer.sizeDelta = new Vector2(battlesContainer.sizeDelta.x, battlesHeight);
            }
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

            var battleNumber = 1;
            mission.battles.ForEach(battle =>
            {
                var battleView = battleViews.ContainsKey(battle.battleId) ? battleViews[battle.battleId] : null;
                if (battleView == null)
                {
                    battleView = Instantiate(missionBattlePrefab, battlesCanvas);
                    battleViews[battle.battleId] = battleView;
                }
                battleView.SetBattle(battle, battleNumber);
                battleNumber++;
            });
        }
    }
}