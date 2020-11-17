using System;
using Backend.Models;
using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class MissionBorderProgressController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private AnimationCurve blinkAnimationCurve;
        [SerializeField] private float blinkingInterval;
        
        [Inject] private ConfigsProvider configsProvider;

        private OfflineBattle battle;
        private PlayerExpedition expedition;

        public void SetMissionBattle(OfflineBattle battle, int battleNumber, int totalBattles)
        {
            var colorsConfig = configsProvider.Get<ColorsConfig>();
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

        public void SetExpedition(ColorsConfig colorsConfig, PlayerExpedition expedition)
        {
            this.expedition = expedition;
            image.color = colorsConfig.missionSuccess;
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);
            UpdateExpedition();
        }

        private void Update()
        {
            if (battle != null && battle.battleStarted && !battle.battleFinished)
            {
                Blink();
            }

            if (expedition != null)
            {
                UpdateExpedition();
            }
        }

        private void UpdateExpedition()
        {
            if (expedition.DoneTime <= DateTime.Now)
            {
                image.fillAmount = 1F;
                Blink();
            }
            else
            {
                var secondsLeft = (float)(expedition.DoneTime - DateTime.Now).TotalSeconds;
                image.fillAmount = 1F - (secondsLeft / expedition.duration);
            }
        }

        private void Blink()
        {
            var timePicker = (Time.unscaledTime % blinkingInterval) / blinkingInterval;
            var alpha = blinkAnimationCurve.Evaluate(timePicker);
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}