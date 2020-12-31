using Backend.Models;
using Backend.Models.Enums;
using Configs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utils;
using Zenject;
using Color = UnityEngine.Color;

namespace Metagame
{
    public class GearAvatarController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Image gearIcon;
        [SerializeField] private Image stars;
        [SerializeField] private Image jewelSlot1;
        [SerializeField] private Image jewelSlot2;
        [SerializeField] private Image jewelSlot3;
        [SerializeField] private Image jewelSlot4;
        [SerializeField] private Image specialJewelSlot;
        [SerializeField] private GameObject specialJewelSlotBackground;
        [SerializeField] private Image border;
        [SerializeField] private Image background;
        [SerializeField] private SpriteAtlas gearAtlas;
        [SerializeField] private SpriteAtlas generalAtlas;
        [SerializeField] private Button button;
        [SerializeField] private GameObject breakdownLayer;
        
        [Inject] private ConfigsProvider configsProvider;

        public void SetGear(Gear gear, float adjustToHeight)
        {
            canvas.ScaleToHeight(adjustToHeight);
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            gearIcon.sprite = gearAtlas.GetSprite($"{gear.set.ToString()}_{gear.type.ToString()}");
            stars.sprite = generalAtlas.GetSprite($"star_{gear.rarity.Stars()}");
            border.color = GetBorderColor(gear, colorsConfig);
            background.color = GetBackgroundColor(gear, colorsConfig);

            if (gear.jewelSlot1 != null)
            {
                jewelSlot1.sprite = gearAtlas.GetSprite($"SLOT_{gear.jewelSlot1.ToString()}{(gear.jewel1Type == null ? "_EMPTY" : "")}");
            }
            else
            {
                jewelSlot1.gameObject.SetActive(false);
            }
            if (gear.jewelSlot2 != null)
            {
                jewelSlot2.sprite = gearAtlas.GetSprite($"SLOT_{gear.jewelSlot2.ToString()}{(gear.jewel2Type == null ? "_EMPTY" : "")}");
            }
            else
            {
                jewelSlot2.gameObject.SetActive(false);
            }
            if (gear.jewelSlot3 != null)
            {
                jewelSlot3.sprite = gearAtlas.GetSprite($"SLOT_{gear.jewelSlot3.ToString()}{(gear.jewel3Type == null ? "_EMPTY" : "")}");
            }
            else
            {
                jewelSlot3.gameObject.SetActive(false);
            }
            if (gear.jewelSlot4 != null)
            {
                jewelSlot4.sprite = gearAtlas.GetSprite($"SLOT_{gear.jewelSlot4.ToString()}{(gear.jewel4Type == null ? "_EMPTY" : "")}");
            }
            else
            {
                jewelSlot4.gameObject.SetActive(false);
            }

            if (gear.specialJewelSlot)
            {
                specialJewelSlot.sprite = gearAtlas.GetSprite($"SLOT_SPECIAL{(gear.specialJewelType == null ? "_EMPTY" : "")}");
            }
            else
            {
                specialJewelSlotBackground.SetActive(false);
            }
            
            ShowBreakdownLayer(false);
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void ShowBreakdownLayer(bool show)
        {
            breakdownLayer.SetActive(show);
        }
        
        private static Color GetBorderColor(Gear gear, ColorsConfig colorsConfig)
        {
            if (gear.jewelSlot1 == null)
            {
                return colorsConfig.gearJewel0;
            }
            if (gear.jewelSlot2 == null)
            {
                return colorsConfig.gearJewel1;
            }
            if (gear.jewelSlot3 == null)
            {
                return colorsConfig.gearJewel2;
            }
            if (gear.jewelSlot4 == null)
            {
                return colorsConfig.gearJewel3;
            }

            return colorsConfig.gearJewel4;
        }

        private static Color GetBackgroundColor(Gear gear, ColorsConfig colorsConfig)
        {
            switch (gear.gearQuality)
            {
                case GearQuality.SHABBY:
                    return colorsConfig.gearSHABBY;
                case GearQuality.RUSTY:
                    return colorsConfig.gearRUSTY;
                case GearQuality.ORDINARY:
                    return colorsConfig.gearORDINARY;
                case GearQuality.USEFUL:
                    return colorsConfig.gearUSEFUL;
                case GearQuality.GOOD:
                    return colorsConfig.gearGOOD;
                case GearQuality.AWESOME:
                    return colorsConfig.gearAWESOME;
                case GearQuality.FLAWLESS:
                    return colorsConfig.gearFLAWLESS;
                case GearQuality.PERFECT:
                    return colorsConfig.gearPERFECT;
                case GearQuality.GODLIKE:
                    return colorsConfig.gearGOOD;
                default:
                    return Color.black;
            }
        }
    }
}