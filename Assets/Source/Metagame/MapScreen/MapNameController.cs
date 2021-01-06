using Backend.Services;
using Backend.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MapScreen
{
    public class MapNameController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        [SerializeField] private GameObject mapAlert;
        [SerializeField] private ChooseMapController chooseMapPrefab;

        [Inject] private MapService mapService;
        [Inject] private SignalBus signalBus;
        [Inject] private PopupCanvasController popupCanvasController;

        private void Start()
        {
            text.text = mapService.CurrentPlayerMap.name;
            mapAlert.SetActive(mapService.HasUnvisitedMineCloseToReset());
            signalBus.Subscribe<CurrentMapSignal>(data =>
            {
                text.text = data.CurrentMap.name;
                mapAlert.SetActive(mapService.HasUnvisitedMineCloseToReset());
            });
            button.onClick.AddListener(() =>
            {
                popupCanvasController.OpenPopup(chooseMapPrefab);
            });
        }
    }
}