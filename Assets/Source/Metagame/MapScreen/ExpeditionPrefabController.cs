using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Configs;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class ExpeditionPrefabController : MonoBehaviour
    {
        [SerializeField] private MissionBorderProgressController borderProgressPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Image inner;
        [SerializeField] private SpriteAtlas vehicleAtlas;
        [SerializeField] private Image vehicleImage;
        [SerializeField] private Button detailButton;
        [SerializeField] private ExpeditionDetailController expeditionDetailPrefab;

        [Inject] private PopupCanvasController popupCanvasController;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private VehicleService vehicleService;

        private PlayerExpedition expedition;

        public void Start()
        {
            detailButton.onClick.AddListener(() =>
            {
                var popup = popupCanvasController.OpenPopup(expeditionDetailPrefab);
                popup.SetExpedition(expedition);
            });
        }

        public void SetExpedition(PlayerExpedition expedition)
        {
            this.expedition = expedition;
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            switch (expedition.rarity)
            {
                case Rarity.SIMPLE:
                    inner.color = colorsConfig.expSimple;
                    break;
                case Rarity.COMMON:
                    inner.color = colorsConfig.expCommon;
                    break;
                case Rarity.UNCOMMON:
                    inner.color = colorsConfig.expUncommon;
                    break;
                case Rarity.RARE:
                    inner.color = colorsConfig.expRare;
                    break;
                case Rarity.EPIC:
                    inner.color = colorsConfig.expEpic;
                    break;
                case Rarity.LEGENDARY:
                    inner.color = colorsConfig.expLegendary;
                    break;
            }
            var vehicle = vehicleService.Vehicle(expedition.vehicleId);
            vehicleImage.sprite = vehicleAtlas.GetSprite(vehicle.avatar);
            
            var border = Instantiate(borderProgressPrefab, canvas);
            border.SetExpedition(colorsConfig, expedition);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}