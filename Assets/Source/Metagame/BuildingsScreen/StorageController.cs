using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame.BuildingsScreen
{
    public class StorageController : MonoBehaviour
    {
        [SerializeField] private Button exitBtn;
        [SerializeField] private Button levelBtn;
        [SerializeField] private Image levelImg;
        [SerializeField] private BuildingUpgradePopup buildingUpgradePrefab;
        [SerializeField] private TMP_Text coinsAmount;
        [SerializeField] private TMP_Text rubiesAmount;
        [SerializeField] private TMP_Text steamAmount;
        [SerializeField] private TMP_Text premiumSteamAmount;
        [SerializeField] private TMP_Text cogwheelsAmount;
        [SerializeField] private TMP_Text premiumCogwheelsAmount;
        [SerializeField] private TMP_Text tokensAmount;
        [SerializeField] private TMP_Text premiumTokensAmount;
        [SerializeField] private TMP_Text metalAmount;
        [SerializeField] private TMP_Text ironAmount;
        [SerializeField] private TMP_Text steelAmount;
        [SerializeField] private TMP_Text woodAmount;
        [SerializeField] private TMP_Text brownCoalAmount;
        [SerializeField] private TMP_Text blackCoalAmount;
        [SerializeField] private TMP_Text woodenKeysAmount;
        [SerializeField] private TMP_Text bronzeKeysAmount;
        [SerializeField] private TMP_Text silverKeysAmount;
        [SerializeField] private TMP_Text goldenKeysAmount;
        [SerializeField] private SpriteAtlas generalAtlas;

        [Inject] private BuildingService buildingService;
        [Inject] private ResourcesService resourcesService;
        [Inject] private PopupCanvasController popupCanvas;
        [Inject] private SignalBus signalBus;
        [Inject] private MetagameManager metagameManager;

        private void Start()
        {
            exitBtn.onClick.AddListener(() => Destroy(gameObject));
            levelBtn.onClick.AddListener(() =>
            {
                var levelPopup = popupCanvas.OpenPopup(buildingUpgradePrefab);
                levelPopup.SetBuildingType(BuildingType.STORAGE);
                levelPopup.OnUpgrade(() =>
                {
                    Destroy(levelPopup.gameObject);
                    Destroy(gameObject);
                    metagameManager.OpenBuildingUpgrade(BuildingType.STORAGE);
                });
            });

            UpdateStorage();
            
            signalBus.Subscribe<BuildingSignal>(UpdateStorage);
            signalBus.Subscribe<ResourcesSignal>(UpdateStorage);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<BuildingSignal>(UpdateStorage);
            signalBus.TryUnsubscribe<ResourcesSignal>(UpdateStorage);
        }

        private void UpdateStorage()
        {
            var storage = buildingService.Building(BuildingType.STORAGE);
            var res = resourcesService.Resources;
            levelImg.sprite = generalAtlas.GetSprite($"level_{storage.level}");
            coinsAmount.text = NumberUtil.RoundAmount(res.coins);
            rubiesAmount.text = NumberUtil.RoundAmount(res.rubies);
            steamAmount.text = $"{NumberUtil.RoundAmount(res.steam)} / {NumberUtil.RoundAmount(res.steamMax)}";
            premiumSteamAmount.text = $"{NumberUtil.RoundAmount(res.premiumSteam)} / {NumberUtil.RoundAmount(res.premiumSteamMax)}";
            cogwheelsAmount.text = $"{NumberUtil.RoundAmount(res.cogwheels)} / {NumberUtil.RoundAmount(res.cogwheelsMax)}";
            premiumCogwheelsAmount.text = $"{NumberUtil.RoundAmount(res.premiumCogwheels)} / {NumberUtil.RoundAmount(res.premiumCogwheelsMax)}";
            tokensAmount.text = $"{NumberUtil.RoundAmount(res.tokens)} / {NumberUtil.RoundAmount(res.tokensMax)}";
            premiumTokensAmount.text = $"{NumberUtil.RoundAmount(res.premiumTokens)} / {NumberUtil.RoundAmount(res.premiumTokensMax)}";
            metalAmount.text = $"{NumberUtil.RoundAmount(res.metal)} / {NumberUtil.RoundAmount(res.metalMax)}";
            ironAmount.text = $"{NumberUtil.RoundAmount(res.iron)} / {NumberUtil.RoundAmount(res.ironMax)}";
            steelAmount.text = $"{NumberUtil.RoundAmount(res.steel)} / {NumberUtil.RoundAmount(res.steelMax)}";
            woodAmount.text = $"{NumberUtil.RoundAmount(res.wood)} / {NumberUtil.RoundAmount(res.woodMax)}";
            brownCoalAmount.text = $"{NumberUtil.RoundAmount(res.brownCoal)} / {NumberUtil.RoundAmount(res.brownCoalMax)}";
            blackCoalAmount.text = $"{NumberUtil.RoundAmount(res.blackCoal)} / {NumberUtil.RoundAmount(res.blackCoalMax)}";
            woodenKeysAmount.text = $"{NumberUtil.RoundAmount(res.woodenKeys)}";
            bronzeKeysAmount.text = $"{NumberUtil.RoundAmount(res.bronzeKeys)}";
            silverKeysAmount.text = $"{NumberUtil.RoundAmount(res.silverKeys)}";
            goldenKeysAmount.text = $"{NumberUtil.RoundAmount(res.goldenKeys)}";
        }
    }
}