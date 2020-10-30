using System.Collections.Generic;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Metagame.MainScreen
{
    public class MissionsController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private MissionPrefabController missionPrefab;
        [SerializeField] private ExpeditionPrefabController expeditionPrefab;
        [Inject] private MissionService missionService;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private SignalBus signalBus;

        private List<MissionPrefabController> missions = new List<MissionPrefabController>();
        private List<ExpeditionPrefabController> expeditions = new List<ExpeditionPrefabController>();

        private void Start()
        {
            UpdateMissions();
            signalBus.Subscribe<MissionSignal>(signal =>
            {
                UpdateMissions();
            });
        }
        
        private void UpdateMissions()
        {
            missions.ForEach(m => m.Remove());
            missions.Clear();
            expeditions.ForEach(e => e.Remove());
            expeditions.Clear();
            foreach (var mission in missionService.Missions)
            {
                var missionOverlay = Instantiate(missionPrefab, canvas);
                missionOverlay.SetMission(mission);
                missions.Add(missionOverlay);
            }

            foreach (var expedition in expeditionService.PlayerExpeditions())
            {
                var expeditionOverlay = Instantiate(expeditionPrefab, canvas);
                expeditionOverlay.SetExpedition(expedition);
                expeditions.Add(expeditionOverlay);
            }
        }
    }
}