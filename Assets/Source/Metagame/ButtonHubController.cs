using System;
using Backend.Services;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame
{
    public class ButtonHubController : MonoBehaviour
    {
        [SerializeField] private Button tasksButton;
        [SerializeField] private GameObject tasksAlert;
        [SerializeField] private Button inboxButton;
        [SerializeField] private GameObject inboxAlert;
        [SerializeField] private TMP_Text inboxAlertText;
        [SerializeField] private Button mapButton;
        [SerializeField] private GameObject mapAlert;
        [SerializeField] private Button buildingsButton;
        [SerializeField] private GameObject buildingsAlert;
        [SerializeField] private Button builderButton;
        [SerializeField] private GameObject builderAlert;

        [Inject] private MetagameManager metagameManager;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        [Inject] private ActivityService activityService;
        [Inject] private AchievementsService achievementsService;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private HeroService heroService;
        [Inject] private InboxService inboxService;
        [Inject] private IncubatorService incubatorService;
        [Inject] private MissionService missionService;
        [Inject] private UpgradeService upgradeService;

        private void Start()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            var colorConfig = configsProvider.Get<ColorsConfig>();
            
            tasksButton.image.color = metagameManager.currentScreen == MainScreenEnum.Tasks ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            tasksAlert.SetActive(HasTaskToClaim());
            
            inboxButton.image.color = metagameManager.currentScreen == MainScreenEnum.Inbox ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            inboxAlert.SetActive(inboxService.Messages.Count > 0);
            inboxAlertText.text = inboxService.Messages.Count.ToString();
            
            mapButton.image.color = metagameManager.currentScreen == MainScreenEnum.Map ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            mapAlert.SetActive(HasFinishedMissionOrExpedition());
            
            buildingsButton.image.color = metagameManager.currentScreen == MainScreenEnum.Buildings ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            buildingsAlert.SetActive(HasBuildingNotification());
            
            builderButton.image.color = metagameManager.currentScreen == MainScreenEnum.Builder ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            builderAlert.SetActive(HasFinishedUpgrade());
        }

        private bool HasTaskToClaim()
        {
            return activityService.HasClaimableActivity() || achievementsService.HasClaimableReward();
        }

        private bool HasFinishedMissionOrExpedition()
        {
            return missionService.Missions.Exists(m => m.missionFinished) ||
                   expeditionService.PlayerExpeditions().Exists(e => e.DoneTime <= DateTime.Now);
        }

        private bool HasBuildingNotification()
        {
            // Barracks: if any hero has skill to claim
            if (heroService.AllHeroes().Exists(h => h.skillPoints > 0))
            {
                return true;
            }
            // Laboratory: if any incubator is finished
            if (incubatorService.Incubators.Exists(i => i.finished))
            {
                return true;
            }

            return false;
        }

        private bool HasFinishedUpgrade()
        {
            return upgradeService.Upgrades.Exists(u => u.finished);
        }
    }
    
}