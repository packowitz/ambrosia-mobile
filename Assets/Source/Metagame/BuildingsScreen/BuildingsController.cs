using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Color = UnityEngine.Color;

namespace Metagame.BuildingsScreen
{
    public class BuildingsController : MonoBehaviour
    {
        [SerializeField] private Button academyBtn;
        [SerializeField] private Image academyImg;
        [SerializeField] private Image academyLock;
        [SerializeField] private TMP_Text academyTxt;
        [SerializeField] private Button arenaBtn;
        [SerializeField] private Image arenaImg;
        [SerializeField] private Image arenaLock;
        [SerializeField] private TMP_Text arenaTxt;
        [SerializeField] private Button barracksBtn;
        [SerializeField] private Image barracksImg;
        [SerializeField] private Image barracksLock;
        [SerializeField] private TMP_Text barracksTxt;
        [SerializeField] private Button bazaarBtn;
        [SerializeField] private Image bazaarImg;
        [SerializeField] private Image bazaarLock;
        [SerializeField] private TMP_Text bazaarTxt;
        [SerializeField] private Button forgeBtn;
        [SerializeField] private Image forgeImg;
        [SerializeField] private Image forgeLock;
        [SerializeField] private TMP_Text forgeTxt;
        [SerializeField] private Button garageBtn;
        [SerializeField] private Image garageImg;
        [SerializeField] private Image garageLock;
        [SerializeField] private TMP_Text garageTxt;
        [SerializeField] private Button guildBtn;
        [SerializeField] private Image guildImg;
        [SerializeField] private Image guildLock;
        [SerializeField] private TMP_Text guildTxt;
        [SerializeField] private Button laboratoryBtn;
        [SerializeField] private Image laboratoryImg;
        [SerializeField] private Image laboratoryLock;
        [SerializeField] private TMP_Text laboratoryTxt;
        [SerializeField] private Button jewelryBtn;
        [SerializeField] private Image jewelryImg;
        [SerializeField] private Image jewelryLock;
        [SerializeField] private TMP_Text jewelryTxt;
        [SerializeField] private Button storageBtn;
        [SerializeField] private Image storageImg;
        [SerializeField] private Image storageLock;
        [SerializeField] private TMP_Text storageTxt;
        [SerializeField] private StorageController storagePrefab;

        public Color lockedColor;
        public Color unlockedColor;

        [Inject] private BuildingService buildingService;
        [Inject] private BuildingCanvasController buildingCanvasController;
        [Inject] private SignalBus signalBus;

        private void Start()
        {
            academyBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto academy");
            });
            arenaBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto arena");
            });
            barracksBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto barracks");
            });
            bazaarBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto bazaar");
            });
            forgeBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto forge");
            });
            garageBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto garage");
            });
            guildBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto guild");
            });
            laboratoryBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto laboratory");
            });
            jewelryBtn.onClick.AddListener(() =>
            {
                Debug.Log("goto jewelry");
            });
            storageBtn.onClick.AddListener(() =>
            {
                if (buildingService.Building(BuildingType.STORAGE) != null)
                {
                    buildingCanvasController.OpenBuilding(storagePrefab);
                }
            });
            UpdateBuildings();
            
            signalBus.Subscribe<BuildingSignal>(UpdateBuildings);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<BuildingSignal>(UpdateBuildings);
        }

        private void UpdateBuildings()
        {
            UpdateBuilding(BuildingType.ACADEMY, academyImg, academyLock, academyTxt);
            UpdateBuilding(BuildingType.ACADEMY, arenaImg, arenaLock, arenaTxt);
            UpdateBuilding(BuildingType.BARRACKS, barracksImg, barracksLock, barracksTxt);
            UpdateBuilding(BuildingType.BAZAAR, bazaarImg, bazaarLock, bazaarTxt);
            UpdateBuilding(BuildingType.FORGE, forgeImg, forgeLock, forgeTxt);
            UpdateBuilding(BuildingType.GARAGE, garageImg, garageLock, garageTxt);
            UpdateBuilding(BuildingType.GUILD, guildImg, guildLock, guildTxt);
            UpdateBuilding(BuildingType.LABORATORY, laboratoryImg, laboratoryLock, laboratoryTxt);
            UpdateBuilding(BuildingType.JEWELRY, jewelryImg, jewelryLock, jewelryTxt);
            UpdateBuilding(BuildingType.STORAGE, storageImg, storageLock, storageTxt);
        }

        private void UpdateBuilding(BuildingType type, Image btnImg, Image locked, TMP_Text nameTxt)
        {
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
            var building = buildingService.Building(type);
            if (building != null)
            {
                btnImg.color = unlockedColor;
                locked.gameObject.SetActive(false);
                nameTxt.gameObject.SetActive(true);
            }
            else
            {
                btnImg.color = lockedColor;
                locked.gameObject.SetActive(true);
                nameTxt.gameObject.SetActive(false);
            }
        }
    }
}