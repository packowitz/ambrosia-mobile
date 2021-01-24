using System;
using Backend.Services;
using Backend.Signal;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame
{
    public class BottomHubController : MonoBehaviour
    {
        [SerializeField] private Button tasksButton;
        [SerializeField] private Image tasksCircle;
        [SerializeField] private GameObject tasksAlert;
        [SerializeField] private Button inboxButton;
        [SerializeField] private Image inboxCircle;
        [SerializeField] private GameObject inboxAlert;
        [SerializeField] private TMP_Text inboxAlertText;
        [SerializeField] private Button mapButton;
        [SerializeField] private Image mapCircle;
        [SerializeField] private GameObject mapAlert;
        [SerializeField] private Button buildingsButton;
        [SerializeField] private Image buildingsCircle;
        [SerializeField] private GameObject buildingsAlert;
        [SerializeField] private Button builderButton;
        [SerializeField] private Image builderCircle;
        [SerializeField] private GameObject builderAlert;

        [Inject] private MetagameManager metagameManager;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        [Inject] private ActivityService activityService;
        [Inject] private TasksService tasksService;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private HeroService heroService;
        [Inject] private InboxService inboxService;
        [Inject] private IncubatorService incubatorService;
        [Inject] private MissionService missionService;
        [Inject] private UpgradeService upgradeService;

        private void Start()
        {
            UpdateButtons();
            mapButton.onClick.AddListener(() =>
            {
                metagameManager.SetMainScreen(MainScreenEnum.Map);
            });
            inboxButton.onClick.AddListener(() =>
            {
                metagameManager.SetMainScreen(MainScreenEnum.Inbox);
            });
            tasksButton.onClick.AddListener(() =>
            {
                metagameManager.SetMainScreen(MainScreenEnum.Tasks);
            });
            buildingsButton.onClick.AddListener(() =>
            {
                metagameManager.SetMainScreen(MainScreenEnum.Buildings);
            });
            builderButton.onClick.AddListener(() =>
            {
                metagameManager.SetMainScreen(MainScreenEnum.Builder);
            });
            
            signalBus.Subscribe<MainScreenChangedSignal>(UpdateButtons);
            signalBus.Subscribe<InboxSignal>(UpdateButtons);
            signalBus.Subscribe<ActivitySignal>(UpdateButtons);
            signalBus.Subscribe<TaskSignal>(UpdateButtons);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<MainScreenChangedSignal>(UpdateButtons);
            signalBus.TryUnsubscribe<InboxSignal>(UpdateButtons);
            signalBus.TryUnsubscribe<ActivitySignal>(UpdateButtons);
            signalBus.TryUnsubscribe<TaskSignal>(UpdateButtons);
        }

        private void UpdateButtons()
        {
            var colorConfig = configsProvider.Get<ColorsConfig>();
            
            tasksCircle.color = metagameManager.CurrentScreen == MainScreenEnum.Tasks ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            tasksAlert.SetActive(HasTaskToClaim());
            
            inboxCircle.color = metagameManager.CurrentScreen == MainScreenEnum.Inbox ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            inboxAlert.SetActive(inboxService.Messages.Count > 0);
            inboxAlertText.text = inboxService.Messages.Count.ToString();
            
            mapCircle.color = metagameManager.CurrentScreen == MainScreenEnum.Map ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            mapAlert.SetActive(HasFinishedMissionOrExpedition());
            
            buildingsCircle.color = metagameManager.CurrentScreen == MainScreenEnum.Buildings ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            buildingsAlert.SetActive(HasBuildingNotification());
            
            builderCircle.color = metagameManager.CurrentScreen == MainScreenEnum.Builder ? colorConfig.bottomHubActive : colorConfig.bottomHubInactive;
            builderAlert.SetActive(HasFinishedUpgrade());
        }

        private bool HasTaskToClaim()
        {
            return activityService.HasClaimableActivity() || tasksService.HasClaimableTask();
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