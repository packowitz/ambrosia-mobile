using Backend.Models;
using Backend.Services;
using Configs;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MainScreen
{
    public class MissionPrefabController : MonoBehaviour
    {
        [SerializeField] private MissionBorderProgressController borderProgressPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private SpriteAtlas vehicleAtlas;
        [SerializeField] private Image vehicleImage;

        [Inject] private VehicleService vehicleService;

        private Mission mission;

        public void SetMission(Mission mission)
        {
            this.mission = mission;
            var battleNumber = 0;
            var vehicle = vehicleService.Vehicle(mission.vehicleId);
            vehicleImage.sprite = vehicleAtlas.GetSprite(vehicle.avatar);
            foreach (var missionBattle in mission.battles)
            {
                var border = Instantiate(borderProgressPrefab, canvas);
                border.SetMissionBattle(missionBattle, battleNumber, mission.totalCount);
                battleNumber++;
            }
        }

        public long MissionId() => mission.id;

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}