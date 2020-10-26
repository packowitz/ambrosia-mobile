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
        [Inject] private MissionService missionService;
        [Inject] private ConfigsProvider configsProvider;

        private void Start()
        {
            foreach (var mission in missionService.Missions)
            {
                var missionOverlay = Instantiate(missionPrefab, canvas);
                missionOverlay.SetMission(configsProvider.Get<ColorsConfig>(), mission);
            }
        }
    }
}