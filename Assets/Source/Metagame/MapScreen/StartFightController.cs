using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Requests;
using Backend.Services;
using Backend.Signal;
using Configs;
using Metagame.HeroAvatar;
using Metagame.VehicleAvatar;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class StartFightController : MonoBehaviour
    {
        [SerializeField] private StartFightResourcePanelController resourcePanel;
        [SerializeField] private RectTransform heroListContainer;
        [SerializeField] private HeroAvatarPrefabController heroAvatarPrefab;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatar;
        [SerializeField] private HeroAvatarPrefabController hero1;
        [SerializeField] private HeroAvatarPrefabController hero2;
        [SerializeField] private HeroAvatarPrefabController hero3;
        [SerializeField] private HeroAvatarPrefabController hero4;
        [SerializeField] private ButtonController cancelBtn;
        [SerializeField] private ButtonResourceController startBtn;
        [SerializeField] private SpriteAtlas resourceAtlas;
        [SerializeField] private ButtonController missionBtn;
        [SerializeField] private GameObject missionPopup;
        [SerializeField] private Slider missionSlider;
        [SerializeField] private ButtonResourceController startMissionBtn;
        [SerializeField] private TMP_Text missionCount;
        [SerializeField] private GameObject fightCanvas;
        [SerializeField] private TMP_Text fightTitle;
        [SerializeField] private TMP_Text fightDescription;
        [SerializeField] private TMP_Text fightXp;
        [SerializeField] private TMP_Text fightAsc;
        [SerializeField] private TMP_Text fightLevel;
        [SerializeField] private RectTransform stagesCanvas;
        [SerializeField] private StartFightPopupStageController stagePrefab;

        [Inject] private FightService fightService;
        [Inject] private TeamService teamService;
        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private MissionService missionService;
        [Inject] private ResourcesService resourcesService;
        [Inject] private ProgressService progressService;
        [Inject] private PropertyService propertyService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        private FightResolved fight;
        private bool missionButtonVisible = true;
        private bool missionPossible = true;
        private readonly StartMissionRequest missionRequest = new StartMissionRequest();
        private bool loading;

        private readonly Dictionary<long, HeroAvatarPrefabController> heroPrefabs = new Dictionary<long, HeroAvatarPrefabController>();

        private void Start()
        {
            loading = true;
            InitHeroes();
            
            hero1.AddClickListener(() =>
            {
                var hero = hero1.GetHero();
                if (hero != null)
                {
                    ToggleHero(hero);
                }
            });
            
            hero2.AddClickListener(() =>
            {
                var hero = hero2.GetHero();
                if (hero != null)
                {
                    ToggleHero(hero);
                }
            });
            
            hero3.AddClickListener(() =>
            {
                var hero = hero3.GetHero();
                if (hero != null)
                {
                    ToggleHero(hero);
                }
            });
            
            hero4.AddClickListener(() =>
            {
                var hero = hero4.GetHero();
                if (hero != null)
                {
                    ToggleHero(hero);
                }
            });
            
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            cancelBtn.SetColor(colorsConfig.buttonDanger);
            cancelBtn.AddClickListener(() => Destroy(gameObject));
            
            missionBtn.SetColor(colorsConfig.buttonSecondary, colorsConfig.buttonSecondaryInactive);
            missionBtn.AddClickListener(() =>
            {
                missionPopup.SetActive(true);
            });
            missionPopup.SetActive(false);
            missionPopup.GetComponent<Button>().onClick.AddListener(() =>
            {
                missionPopup.SetActive(false);
            });
            missionSlider.onValueChanged.AddListener(value =>
            {
                UpdateMissionCount(Convert.ToInt32(value));
            });
            startMissionBtn.SetColor(colorsConfig.buttonSuccess, colorsConfig.buttonSuccessInactive);
            startMissionBtn.AddClickListener(() =>
            {
                missionRequest.vehicleId = vehicleAvatar.Vehicle.id;
                missionRequest.hero1Id = hero1.GetHero()?.id;
                missionRequest.hero2Id = hero2.GetHero()?.id;
                missionRequest.hero3Id = hero3.GetHero()?.id;
                missionRequest.hero4Id = hero4.GetHero()?.id;
                startMissionBtn.ShowIndicator();
                missionService.StartMission(missionRequest, data =>
                {
                    Destroy(gameObject);
                });
            });
            
            startBtn.SetColor(colorsConfig.buttonSuccess, colorsConfig.buttonSuccessInactive);
            
            signalBus.Subscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void ConsumeResourcesSignal(ResourcesSignal signal)
        {
            UpdateMissionMinMax();
            CheckButton();
        }

        public void SetMapTile(PlayerMap map, PlayerMapTile tile)
        {
            if (tile.fightId == null)
            {
                return;
            }

            missionRequest.type = $"C_{map.mapId}";
            missionRequest.mapId = map.mapId;
            missionRequest.posX = tile.posX;
            missionRequest.posY = tile.posY;
            fightTitle.text = $"{map.name} {tile.posX}x{tile.posY}";
            fightService.GetFight((long) tile.fightId, data =>
            {
                fight = data;
                loading = false;
                UpdateFight();
                CheckButton();
            });

            if (tile.fightRepeatable != true)
            {
                Destroy(missionBtn.gameObject);
                missionButtonVisible = false;
                missionPossible = false;
            }

            missionPossible = tile.fightRepeatable == true && tile.victoriousFight == true;
            
            InitTeam();
        }

        private void InitHeroes()
        {
            heroService.AllHeroes().ForEach(hero =>
            {
                var heroPrefab = Instantiate(heroAvatarPrefab, heroListContainer);
                heroPrefab.SetHero(hero, indicateAvailability: true);
                if (hero.IsAvailable())
                {
                    heroPrefab.SetActive(IsHeroSelected(hero));
                    heroPrefab.AddClickListener(() => { ToggleHero(hero); });
                }

                heroPrefabs.Add(hero.id, heroPrefab);
            });
        }
        
        private void InitTeam()
        {
            var team = teamService.Team(missionRequest.type);
            
            var vehicle = vehicleService.AvailableVehicle(team?.vehicleId) ?? vehicleService.AvailableVehicle();
            vehicleAvatar.SetVehicle(vehicle);
            vehicleAvatar.ActivateNextAvailableOnClick(UpdateXpAndAsc);

            hero1.SetHero(heroService.AvailableHero(team?.hero1Id));
            hero2.SetHero(heroService.AvailableHero(team?.hero2Id));
            hero3.SetHero(heroService.AvailableHero(team?.hero3Id));
            hero4.SetHero(heroService.AvailableHero(team?.hero4Id));
            
            CheckButton();
        }

        private void UpdateFight()
        {
            resourcePanel.SetResourceType(fight.resourceType);
            var resourceIcon = resourceAtlas.GetSprite(fight.resourceType.ToString());
            startBtn.SetResource(resourceIcon, $"{fight.costs}");
            startMissionBtn.SetResourceIcon(resourceIcon);
            UpdateMissionMinMax();
            UpdateMissionCount(MaxMissionsPossible());
            
            fightCanvas.SetActive(true);
            fightDescription.text = fight.description;
            fightLevel.text = $"Difficulty {fight.level}";
            UpdateXpAndAsc();
            
            fight.stages.ForEach(stage =>
            {
                var stageCanvas = Instantiate(stagePrefab, stagesCanvas);
                stageCanvas.SetStage(stage);
            });
        }

        private void UpdateXpAndAsc()
        {
            if (fight != null)
            {
                var xpBoost = progressService.Progress.battleXpBoost;
                var ascBoost = 0;
                if (vehicleAvatar.Vehicle != null)
                {
                    xpBoost += propertyService.VehicleStat(vehicleAvatar.Vehicle, VehicleStat.BATTLE_XP);
                    ascBoost += propertyService.VehicleStat(vehicleAvatar.Vehicle, VehicleStat.BATTLE_ASC_POINTS);
                }

                fightXp.text = $"XP {fight.xp + ((fight.xp * xpBoost) / 100)}";
                fightAsc.text = $"Asc {fight.ascPoints + ((fight.ascPoints * ascBoost) / 100)}";
            }
        }

        private void UpdateMissionMinMax()
        {
            missionSlider.maxValue = MaxMissionsPossible();
        }

        private void UpdateMissionCount(int count)
        {
            missionRequest.battleTimes = count;
            missionSlider.value = count;
            startMissionBtn.SetResourceAmount($"{fight.costs * count}");
            missionCount.text = $"{count}";
            startMissionBtn.SetInteractable(count > 0);
        }

        private bool IsHeroSelected(Hero hero)
        {
            var selected = hero1.GetHero()?.id == hero.id || 
                           hero2.GetHero()?.id == hero.id || 
                           hero3.GetHero()?.id == hero.id || 
                           hero4.GetHero()?.id == hero.id;
            return selected;
        }
        
        private void ToggleHero(Hero hero)
        {
            if (hero1.GetHero()?.id == hero.id)
            {
                hero1.SetHero(null);
                heroPrefabs[hero.id].SetActive(false);
            }
            else if (hero2.GetHero()?.id == hero.id)
            {
                hero2.SetHero(null);
                heroPrefabs[hero.id].SetActive(false);
            }
            else if (hero3.GetHero()?.id == hero.id)
            {
                hero3.SetHero(null);
                heroPrefabs[hero.id].SetActive(false);
            }
            else if (hero4.GetHero()?.id == hero.id)
            {
                hero4.SetHero(null);
                heroPrefabs[hero.id].SetActive(false);
            }

            else if (hero1.GetHero() == null)
            {
                hero1.SetHero(hero);
                heroPrefabs[hero.id].SetActive(true);
            }
            else if (hero2.GetHero() == null)
            {
                hero2.SetHero(hero);
                heroPrefabs[hero.id].SetActive(true);
            }
            else if (hero3.GetHero() == null)
            {
                hero3.SetHero(hero);
                heroPrefabs[hero.id].SetActive(true);
            }
            else if (hero4.GetHero() == null)
            {
                hero4.SetHero(hero);
                heroPrefabs[hero.id].SetActive(true);
            }
            CheckButton();
        }

        private bool VehicleSelected()
        {
            return vehicleAvatar.Vehicle != null;
        }

        private bool AtLeastOneHeroSelected()
        {
            return hero1.GetHero() != null || hero2.GetHero() != null || hero3.GetHero() != null || hero4.GetHero() != null;
        }

        private int MaxMissionsPossible()
        {
            if (fight == null)
            {
                return 0;
            }
            var maxFights = resourcesService.ResourceAmount(fight.resourceType) / fight.costs;
            var maxBattles = progressService.Progress.maxOfflineBattlesPerMission;
            return maxFights < maxBattles ? maxFights : maxBattles;
        }
        
        private void CheckButton()
        {
            if (missionButtonVisible)
            {
                missionBtn.SetInteractable(!loading && missionPossible && VehicleSelected() && AtLeastOneHeroSelected());
            }
            startBtn.SetInteractable(!loading && AtLeastOneHeroSelected() && MaxMissionsPossible() > 0);
        }
    }
}