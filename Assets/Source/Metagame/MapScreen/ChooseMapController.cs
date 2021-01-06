using System.Linq;
using Backend.Models.Enums;
using Backend.Services;
using Configs;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Color = UnityEngine.Color;

namespace Metagame.MapScreen
{
    public class ChooseMapController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text mapName;
        [SerializeField] private Button favButton;
        [SerializeField] private Image favImage;
        [SerializeField] private Image campaignBackground;
        [SerializeField] private Button campaignButton;
        [SerializeField] private Image dungeonsBackground;
        [SerializeField] private Button dungeonsButton;
        [SerializeField] private Image minesBackground;
        [SerializeField] private Button minesButton;
        [SerializeField] private GameObject minesAlert;
        [SerializeField] private RectTransform mapsCanvas;
        [SerializeField] private ChooseMapTextPrefab chooseMapTextPrefab;

        [Inject] private MapService mapService;
        [Inject] private ConfigsProvider configsProvider;

        private MapType mapType = MapType.CAMPAIGN;

        private void Start()
        {
            dismiss.onClick.AddListener(() => { Destroy(gameObject); });
            favButton.onClick.AddListener(() => { mapService.ToggleFavorite(data => { UpdatePopup(); }); });
            campaignButton.onClick.AddListener(() =>
            {
                mapType = MapType.CAMPAIGN;
                UpdatePopup();
            });
            dungeonsButton.onClick.AddListener(() =>
            {
                mapType = MapType.DUNGEON;
                UpdatePopup();
            });
            minesButton.onClick.AddListener(() =>
            {
                mapType = MapType.MINE;
                UpdatePopup();
            });

            UpdatePopup();
        }

        private void UpdatePopup()
        {
            mapName.text = mapService.CurrentPlayerMap.name;
            UpdateMapList();
            SetFavorite();
            UpdateTabs();
        }

        private void SetFavorite()
        {
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            favImage.color = mapService.CurrentPlayerMap.favorite ? colorsConfig.mapFavorite : colorsConfig.mapNoFavorite;
        }

        private void UpdateMapList()
        {
            mapsCanvas.DetachChildren();
            mapsCanvas.gameObject.SetActive(false);
            var maps = mapService.PlayerMaps.Where(map => map.type == mapType && map.favorite).ToList();
            if (maps.IsEmpty())
            {
                var emptyMapList = Instantiate(chooseMapTextPrefab, mapsCanvas);
                emptyMapList.SetText("No maps marked as favorite", true);
            }
            else
            {
                maps.ForEach(map =>
                {
                    var mapNameInstance = Instantiate(chooseMapTextPrefab, mapsCanvas);
                    mapNameInstance.SetText(map.name);
                    mapNameInstance.AddClickListener(() =>
                    {
                        mapService.ChangeMapTo(map.mapId);
                        Destroy(gameObject);
                    });
                });
            }
            mapsCanvas.gameObject.SetActive(true);
        }

        private void UpdateTabs()
        {
            campaignBackground.color = new Color {a = mapType == MapType.CAMPAIGN ? 0.75f : 0.35f, r = 1f, g = 1f, b = 1f};
            dungeonsBackground.color = new Color {a = mapType == MapType.DUNGEON ? 0.75f : 0.35f, r = 1f, g = 1f, b = 1f};
            minesBackground.color = new Color {a = mapType == MapType.MINE ? 0.75f : 0.35f, r = 1f, g = 1f, b = 1f};
            minesAlert.SetActive(mapService.HasUnvisitedMineCloseToReset());
        }
    }
}