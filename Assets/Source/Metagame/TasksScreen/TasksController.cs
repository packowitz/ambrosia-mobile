using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Metagame.TasksScreen
{
    public class TasksController : MonoBehaviour
    {
        [SerializeField] private TabController oddJobsTab;
        [SerializeField] private GameObject activityTabCanvas;
        [SerializeField] private TabController tasksTab;
        [SerializeField] private GameObject tasksTabCanvas;
        [SerializeField] private RectTransform oddJobsCanvas;
        [SerializeField] private RectTransform tasksCanvas;
        [SerializeField] private OddJobController oddJobPrefab;
        [SerializeField] private TaskController taskPrefab;
        [SerializeField] private TaskCategoryController taskCategoryPrefab;
        [SerializeField] private ConfirmPopupController confirmPrefab;
        [SerializeField] private LootedController lootedPrefab;

        public List<DailyActivityController> days;
        
        [Inject] private SignalBus signalBus;
        [Inject] private ActivityService activityService;
        [Inject] private TasksService tasksService;
        [Inject] private ProgressService progressService;
        [Inject] private PropertyService propertyService;
        [Inject] private PopupCanvasController popupCanvasController;

        private List<OddJobController> oddJobs = new List<OddJobController>();
        private List<GameObject> tasks = new List<GameObject>();

        private void Start()
        {
            oddJobsTab.AddClickListener(() =>
            {
                oddJobsTab.SetActive();
                activityTabCanvas.SetActive(true);
                tasksTab.SetActive(false);
                tasksTabCanvas.SetActive(false);
            });
            tasksTab.AddClickListener(() =>
            {
                oddJobsTab.SetActive(false);
                activityTabCanvas.SetActive(false);
                tasksTab.SetActive();
                tasksTabCanvas.SetActive(true);
            });
            days.ForEach(day =>
            {
                
                day.AddClickListener(() =>
                {
                    if (activityService.DailyActivity.Claimable(day.day))
                    {
                        activityService.ClaimDaily(day.day, data =>
                        {
                            if (data.looted != null)
                            {
                                var lootedPopup = popupCanvasController.OpenPopup(lootedPrefab);
                                lootedPopup.SetLooted(data.looted);
                            }
                        });
                    }
                });
            });
            
            UpdateTabAlerts();
            UpdateActivityTab();
            UpdateTasksTab();
            
            signalBus.Subscribe<ActivitySignal>(ConsumeActivitySignal);
            signalBus.Subscribe<TaskSignal>(ConsumeTaskSignal);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<ActivitySignal>(ConsumeActivitySignal);
            signalBus.TryUnsubscribe<TaskSignal>(ConsumeTaskSignal);
        }

        private void ConsumeActivitySignal(ActivitySignal signal)
        {
            UpdateTabAlerts();
            UpdateActivityTab();
        }

        private void ConsumeTaskSignal(TaskSignal signal)
        {
            UpdateTabAlerts();
            UpdateTasksTab();
        }

        private void UpdateTabAlerts()
        {
            oddJobsTab.ShowAlert(activityService.HasClaimableActivity());
            tasksTab.ShowAlert(tasksService.HasClaimableTask());
        }

        private void UpdateActivityTab()
        {
            days.ForEach(day =>
            {
                day.SetLootItem(propertyService.DailyReward(day.day));
                day.SetClaimable(activityService.DailyActivity.Claimable(day.day));
                day.SetActiveDay(day.day == activityService.DailyActivity.today);
                day.SetClaimed(activityService.DailyActivity.Claimed(day.day));
            });
            
            oddJobs.ForEach(j => Destroy(j.gameObject));
            oddJobs.Clear();
            activityService.OddJobs.ForEach(oddJob =>
            {
                var jobView = Instantiate(oddJobPrefab, oddJobsCanvas);
                jobView.SetJob(oddJob);
                jobView.OnClaim(() =>
                {
                    activityService.ClaimOddJob(oddJob, data =>
                    {
                        if (data.looted != null)
                        {
                            var lootedPopup = popupCanvasController.OpenPopup(lootedPrefab);
                            lootedPopup.SetLooted(data.looted);
                        }
                    });
                });
                jobView.OnDelete(() =>
                {
                    var confirm = popupCanvasController.OpenPopup(confirmPrefab);
                    confirm.SetText("Are you sure to cancel that odd job?");
                    confirm.OnConfirm(() =>
                    {
                        confirm.ShowIndicator();
                        activityService.RemoveOddJob(oddJob, data =>
                        {
                            confirm.ClosePopup();
                        });
                    });
                });
                oddJobs.Add(jobView);
            });

            var emptySlots = progressService.Progress.numberOddJobs - activityService.OddJobs.Count;
            for (var i = 0; i < emptySlots; i++)
            {
                var jobView = Instantiate(oddJobPrefab, oddJobsCanvas);
                jobView.SetJob(null);
                oddJobs.Add(jobView);
            }
        }

        private void UpdateTasksTab()
        {
            tasks.ForEach(Destroy);
            tasks.Clear();
            foreach (TaskCategory taskCategory in Enum.GetValues(typeof(TaskCategory)))
            {
                var taskCategoryName = Instantiate(taskCategoryPrefab, tasksCanvas);
                taskCategoryName.SetCategory(taskCategory);
                tasks.Add(taskCategoryName.gameObject);
                foreach (var taskCluster in tasksService.TaskClusters.Where(t => t.category == taskCategory))
                {
                    var task = GetTask(taskCluster);
                    var taskView = Instantiate(taskPrefab, tasksCanvas);
                    taskView.SetTask(taskCluster, task, tasksService.AchievementAmount(task.taskType));
                    taskView.OnClaim(() =>
                    {
                        tasksService.ClaimTask(taskCluster, data =>
                        {
                            if (data.looted != null)
                            {
                                var lootedPopup = popupCanvasController.OpenPopup(lootedPrefab);
                                lootedPopup.SetLooted(data.looted);
                            }
                        });
                    });
                    tasks.Add(taskView.gameObject);
                }
            }
        }

        private Task GetTask(TaskCluster taskCluster)
        {
            var playerTask = tasksService.PlayerTasks.Find(t => t.taskClusterId == taskCluster.id);
            if (playerTask != null) {
                return taskCluster.tasks.Find(t => t.number == playerTask.currentTaskNumber);
            }

            return taskCluster.tasks[0];
        }
    }
}