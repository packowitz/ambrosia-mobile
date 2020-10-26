using Backend.Models;
using Configs;
using UnityEngine;

namespace Metagame.MainScreen
{
    public class MissionPrefabController : MonoBehaviour
    {
        [SerializeField] private MissionBorderProgressController borderProgressPrefab;
        [SerializeField] private RectTransform canvas;

        private Mission mission;

        public void SetMission(ColorsConfig colorsConfig, Mission mission)
        {
            this.mission = mission;
            var battleNumber = 0;
            foreach (var missionBattle in mission.battles)
            {
                var border = Instantiate(borderProgressPrefab, canvas);
                border.SetMissionBattle(colorsConfig, missionBattle, battleNumber, mission.totalCount);
                battleNumber++;
            }
        }
    }
}