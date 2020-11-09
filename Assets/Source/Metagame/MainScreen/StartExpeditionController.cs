using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Services;
using Metagame.HeroAvatar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame.MainScreen
{
    public class StartExpeditionController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text available;
        [SerializeField] private TMP_Text description;
        [SerializeField] private RectTransform heroListContainer;
        [SerializeField] private HeroAvatarPrefabController heroAvatarPrefab;

        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;
        [Inject] private TeamService teamService;

        private const string TEAM_TYPE = "Expedition";

        private Expedition expedition;

        private Vehicle vehicle;
        private Hero hero1;
        private Hero hero2;
        private Hero hero3;
        private Hero hero4;

        private Dictionary<long, HeroAvatarPrefabController> heroPrefabs = 
            new Dictionary<long, HeroAvatarPrefabController>();

        private void Start()
        {
            dismiss.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            available.text = "Disappears in " + TimeSpan.FromSeconds(expedition.secondsAvailable).TimerWithUnit();
        }

        public void SetExpedition(Expedition expedition)
        {
            this.expedition = expedition;
            InitTeam();
            title.text = expedition.expeditionBase.name;
            description.text = expedition.expeditionBase.description;

            heroService.AllHeroes().ForEach(hero =>
            {
                var heroPrefab = Instantiate(heroAvatarPrefab, heroListContainer);
                heroPrefab.SetHero(hero);
                heroPrefab.SetActive(IsHeroSelected(hero));
                heroPrefab.AddClickListener(() =>
                {
                    heroPrefab.SetActive(ToggleHero(hero));
                });
                heroPrefabs.Add(hero.id, heroPrefab);
            });
        }

        private void InitTeam()
        {
            var team = teamService.Team(TEAM_TYPE);
            if (team != null)
            {
                vehicle = vehicleService.Vehicle(team.vehicleId);
                hero1 = heroService.Hero(team.hero1Id);
                hero2 = heroService.Hero(team.hero2Id);
                hero3 = heroService.Hero(team.hero3Id);
                hero4 = heroService.Hero(team.hero4Id);
            }

            if (vehicle == null)
            {
                vehicle = vehicleService.AvailableVehicle();
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
                hero1 = null;
                return false;
            }
            if (hero2?.id == hero.id)
            {
                hero2 = null;
                return false;
            }
            if (hero3?.id == hero.id)
            {
                hero3 = null;
                return false;
            }
            if (hero4?.id == hero.id)
            {
                hero4 = null;
                return false;
            }

            if (hero1 == null)
            {
                hero1 = hero;
                return true;
            }
            if (hero2 == null)
            {
                hero2 = hero;
                return true;
            }
            if (hero3 == null)
            {
                hero3 = hero;
                return true;
            }
            if (hero4 == null)
            {
                hero4 = hero;
                return true;
            }

            return false;
        }
    }
}