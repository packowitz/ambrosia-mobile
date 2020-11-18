using System;
using System.Collections.Generic;
using Backend;
using Backend.Models;
using Backend.Services;
using Metagame.HeroAvatar;
using Metagame.VehicleAvatar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame.MapScreen
{
    public class StartExpeditionController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text available;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text durationText;
        [SerializeField] private TMP_Text xpText;
        [SerializeField] private RectTransform heroListContainer;
        [SerializeField] private HeroAvatarPrefabController heroAvatarPrefab;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatarPrefab;
        [SerializeField] private RectTransform vehicleCanvas;
        [SerializeField] private RectTransform hero1Canvas;
        [SerializeField] private RectTransform hero2Canvas;
        [SerializeField] private RectTransform hero3Canvas;
        [SerializeField] private RectTransform hero4Canvas;
        [SerializeField] private Button startButton;
        [SerializeField] private TMP_Text startButtonText;
        [SerializeField] private Image loadingIndicator;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private TeamService teamService;
        [Inject] private ProgressService progressService;
        [Inject] private ServerAPI serverAPI;

        private const string TEAM_TYPE = "Expedition";

        private Expedition expedition;

        private Hero hero1;
        private HeroAvatarPrefabController hero1Prefab;
        private Hero hero2;
        private HeroAvatarPrefabController hero2Prefab;
        private Hero hero3;
        private HeroAvatarPrefabController hero3Prefab;
        private Hero hero4;
        private HeroAvatarPrefabController hero4Prefab;

        private readonly Dictionary<long, HeroAvatarPrefabController> heroPrefabs = new Dictionary<long, HeroAvatarPrefabController>();
        private bool loading;

        private void Start()
        {
            dismiss.onClick.AddListener(() =>
            {
                if (!loading)
                {
                    Destroy(gameObject);
                }
            });
            startButton.onClick.AddListener(StartExpedition);
            loadingIndicator.enabled = false;
        }

        private void Update()
        {
            var timeLeft = expedition.AvailableTime - DateTime.Now;
            available.text = "Disappears in " + timeLeft.TimerWithUnit();
        }

        public void SetExpedition(Expedition exp)
        {
            expedition = exp;
            var expSpeedup = progressService.Progress.expeditionSpeed - 100;
            InitTeam();
            title.text = expedition.expeditionBase.name;
            description.text = expedition.expeditionBase.description;
            durationText.text = $"Duration: {expedition.expeditionBase.durationHours}h{(expSpeedup > 0 ? $" ({expSpeedup}% speedup)" : "")}";
            xpText.text = $"{expedition.expeditionBase.xp} XP";

            heroService.AllHeroes().ForEach(hero =>
            {
                var heroPrefab = Instantiate(heroAvatarPrefab, heroListContainer);
                heroPrefab.SetHero(hero, indicateAvailability: true);
                if (hero.IsAvailable())
                {
                    heroPrefab.SetActive(IsHeroSelected(hero));
                    heroPrefab.AddClickListener(() => { heroPrefab.SetActive(ToggleHero(hero)); });
                }

                heroPrefabs.Add(hero.id, heroPrefab);
            });
        }

        private void InitTeam()
        {
            var team = teamService.Team(TEAM_TYPE);
            Vehicle vehicle = null;
            if (team != null)
            {
                vehicle = vehicleService.AvailableVehicle(team.vehicleId);
                hero1 = heroService.AvailableHero(team.hero1Id);
                hero2 = heroService.AvailableHero(team.hero2Id);
                hero3 = heroService.AvailableHero(team.hero3Id);
                hero4 = heroService.AvailableHero(team.hero4Id);
            }
            
            SelectHero1(hero1);
            SelectHero2(hero2);
            SelectHero3(hero3);
            SelectHero4(hero4);

            if (vehicle == null)
            {
                vehicle = vehicleService.AvailableVehicle();
            }
            vehicleAvatarPrefab = Instantiate(vehicleAvatarPrefab, vehicleCanvas);
            vehicleAvatarPrefab.SetVehicle(vehicle);
            vehicleAvatarPrefab.ActivateNextAvailableOnClick();
            
            CheckButton();
        }

        private void StartExpedition()
        {
            Debug.Log("Start Button clicked");
            loading = true;
            startButton.interactable = false;
            startButtonText.enabled = false;
            loadingIndicator.enabled = true;

            var team = new Team
            {
                type = TEAM_TYPE,
                vehicleId = vehicleAvatarPrefab.Vehicle.id,
                hero1Id = hero1?.id,
                hero2Id = hero2?.id,
                hero3Id = hero3?.id,
                hero4Id = hero4?.id
            };
            serverAPI.DoPost($"/expedition/{expedition.id}/start", team, response =>
            {
                Destroy(gameObject);
            });
        }

        private void SelectHero1(Hero hero)
        {
            if (hero1Prefab != null)
            {
                hero1Prefab.Remove();
            }
            hero1Prefab = Instantiate(heroAvatarPrefab, hero1Canvas);
            hero1 = hero;
            hero1Prefab.SetHero(hero);
            CheckButton();
            if (hero != null)
            {
                hero1Prefab.AddClickListener(() =>
                {
                    heroPrefabs[hero.id].SetActive(false);
                    SelectHero1(null);
                });
            }
        }

        private void SelectHero2(Hero hero)
        {
            if (hero2Prefab != null)
            {
                hero2Prefab.Remove();
            }
            hero2Prefab = Instantiate(heroAvatarPrefab, hero2Canvas);
            hero2 = hero;
            hero2Prefab.SetHero(hero);
            CheckButton();
            if (hero != null)
            {
                hero2Prefab.AddClickListener(() =>
                {
                    heroPrefabs[hero.id].SetActive(false);
                    SelectHero2(null);
                });
            }
        }

        private void SelectHero3(Hero hero)
        {
            if (hero3Prefab != null)
            {
                hero3Prefab.Remove();
            }
            hero3Prefab = Instantiate(heroAvatarPrefab, hero3Canvas);
            hero3 = hero;
            hero3Prefab.SetHero(hero);
            CheckButton();
            if (hero != null)
            {
                hero3Prefab.AddClickListener(() =>
                {
                    heroPrefabs[hero.id].SetActive(false);
                    SelectHero3(null);
                });
            }
        }

        private void SelectHero4(Hero hero)
        {
            if (hero4Prefab != null)
            {
                hero4Prefab.Remove();
            }
            hero4Prefab = Instantiate(heroAvatarPrefab, hero4Canvas);
            hero4 = hero;
            hero4Prefab.SetHero(hero);
            CheckButton();
            if (hero != null)
            {
                hero4Prefab.AddClickListener(() =>
                {
                    heroPrefabs[hero.id].SetActive(false);
                    SelectHero4(null);
                });
            }
        }

        private bool IsHeroSelected(Hero hero)
        {
            var selected = hero1?.id == hero.id || hero2?.id == hero.id || hero3?.id == hero.id || hero4?.id == hero.id;
            return selected;
        }

        private bool ToggleHero(Hero hero)
        {
            if (hero1?.id == hero.id)
            {
                SelectHero1(null);
                return false;
            }
            if (hero2?.id == hero.id)
            {
                SelectHero2(null);
                return false;
            }
            if (hero3?.id == hero.id)
            {
                SelectHero3(null);
                return false;
            }
            if (hero4?.id == hero.id)
            {
                SelectHero4(null);
                return false;
            }

            if (hero1 == null)
            {
                SelectHero1(hero);
                return true;
            }
            if (hero2 == null)
            {
                SelectHero2(hero);
                return true;
            }
            if (hero3 == null)
            {
                SelectHero3(hero);
                return true;
            }
            if (hero4 == null)
            {
                SelectHero4(hero);
                return true;
            }

            return false;
        }

        private void CheckButton()
        {
            if (loading || vehicleAvatarPrefab.Vehicle == null || (hero1 == null && hero2 == null && hero3 == null && hero4 == null))
            {
                startButton.interactable = false;
            }
            else
            {
                startButton.interactable = true;
            }
        }
    }
}