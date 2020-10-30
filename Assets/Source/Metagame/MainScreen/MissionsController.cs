using Backend.Services;
using Configs;
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
        [Inject] private VehicleService vehicleService;
        [Inject] private ConfigsProvider configsProvider;

        private void Start()
        {
            foreach (var mission in missionService.Missions)
            {
                var missionOverlay = Instantiate(missionPrefab, canvas);
                missionOverlay.colorsConfig = configsProvider.Get<ColorsConfig>();
                missionOverlay.vehicleService = vehicleService;
                missionOverlay.SetMission(mission);
            }

            foreach (var expedition in expeditionService.PlayerExpeditions())
            {
                var expeditionOverlay = Instantiate(expeditionPrefab, canvas);
                expeditionOverlay.colorsConfig = configsProvider.Get<ColorsConfig>();
                expeditionOverlay.vehicleService = vehicleService;
                expeditionOverlay.SetExpedition(expedition);
            }
        }
    }
}