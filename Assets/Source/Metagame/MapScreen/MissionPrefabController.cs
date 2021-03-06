using Backend.Models;
using Backend.Services;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class MissionPrefabController : MonoBehaviour
    {
        [SerializeField] private MissionBorderProgressController borderProgressPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private SpriteAtlas vehicleAtlas;
        [SerializeField] private Image vehicleImage;
        [SerializeField] private Button detailButton;
        [SerializeField] private MissionDetailController missionDetailPrefab;

        [Inject] private VehicleService vehicleService;
        [Inject] private PopupCanvasController popupCanvasController;

        private Mission mission;

        private void Start()
        {
            detailButton.onClick.AddListener(() =>
            {
                var missionPopup = popupCanvasController.OpenPopup(missionDetailPrefab);
                missionPopup.SetMission(mission);
            });
        }

        public void SetMission(Mission m)
        {
            mission = m;
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