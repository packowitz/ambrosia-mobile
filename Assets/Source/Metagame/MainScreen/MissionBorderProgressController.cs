using System;
using Backend.Models;
using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MainScreen
{
    public class MissionBorderProgressController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private AnimationCurve blinkAnimationCurve;
        [SerializeField] private float blinkingInterval;

        private OfflineBattle battle;

        public void SetMissionBattle(ColorsConfig colorsConfig, OfflineBattle battle, int battleNumber, int totalBattles)
        {
            this.battle = battle;
            rectTransform.rotation = Quaternion.Euler(0, 0, (-360F / totalBattles) * battleNumber);
            image.fillAmount = (1F / totalBattles) - 0.01F;
            if (battle.battleFinished)
            {
                image.color = battle.battleSuccess ? colorsConfig.missionSuccess : colorsConfig.missionFailed;
            }
            else
            {
                image.color = battle.battleStarted ? colorsConfig.missionSuccess : colorsConfig.missionIdle;
            }
        }

        private void Update()
        {
            if (battle.battleStarted && !battle.battleFinished)
            {
                var timePicker = (Time.unscaledTime % blinkingInterval) / blinkingInterval;
                var alpha = blinkAnimationCurve.Evaluate(timePicker);
                var color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}