using System;
using Backend.Models;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MainScreen
{
    public class AvailableExpeditionPrefabController : MonoBehaviour
    {
        [SerializeField] private Image border;
        [SerializeField] private TMP_Text durationText;
        [Inject] private ConfigsProvider configsProvider;

        public void SetExpedition(Expedition expedition)
        {
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

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}