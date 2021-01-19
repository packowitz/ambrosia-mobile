using Backend.Models;
using Backend.Models.Enums;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class AvailableExpeditionPrefabController : MonoBehaviour
    {
        [SerializeField] private Button start;
        [SerializeField] private StartExpeditionController startExpeditionPrefab;
        [SerializeField] private Image border;
        [SerializeField] private TMP_Text durationText;
        
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private PopupCanvasController popupCanvasController;

        private Expedition expedition;

        private void Start()
        {
            start.onClick.AddListener(() =>
            {
                var popup = popupCanvasController.OpenPopup(startExpeditionPrefab);
                popup.SetExpedition(expedition);
            });
        }

        public void SetExpedition(Expedition exp)
        {
            expedition = exp;
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            switch (expedition.expeditionBase.rarity)
            {
                case Rarity.SIMPLE:
                    border.color = colorsConfig.expSimpleBorder;
                    break;
                case Rarity.COMMON:
                    border.color = colorsConfig.expCommonBorder;
                    break;
                case Rarity.UNCOMMON:
                    border.color = colorsConfig.expUncommonBorder;
                    break;
                case Rarity.RARE:
                    border.color = colorsConfig.expRareBorder;
                    break;
                case Rarity.EPIC:
                    border.color = colorsConfig.expEpicBorder;
                    break;
                case Rarity.LEGENDARY:
                    border.color = colorsConfig.expLegendaryBorder;
                    break;
            }

            durationText.text = expedition.expeditionBase.durationHours.ToString();
        }
    }
}