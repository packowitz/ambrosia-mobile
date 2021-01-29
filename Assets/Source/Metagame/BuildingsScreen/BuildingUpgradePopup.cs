using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Cysharp.Threading.Tasks;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame.BuildingsScreen
{
    public class BuildingUpgradePopup : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private Image levelImg;
        [SerializeField] private TMP_Text buildingName;
        [SerializeField] private RectTransform levelButtonCanvas;
        [SerializeField] private BuildingLevelButtonController levelButtonPrefab;
        [SerializeField] private TMP_Text levelTitle;
        [SerializeField] private RectTransform levelBoni;
        [SerializeField] private BonusTextController bonusTextPrefab;
        [SerializeField] private RectTransform costsCanvas;
        [SerializeField] private AnyResourcePanel resourcePrefab;
        [SerializeField] private ButtonController upgradeButton;
        [SerializeField] private SpriteAtlas generalAtlas;
        [SerializeField] private SpriteAtlas resourcesAtlas;

        [Inject] private PropertyService propertyService;
        [Inject] private BuildingService buildingService;

        private BuildingType buildingType;
        private readonly List<BuildingLevelButtonController> levelButtons = new List<BuildingLevelButtonController>();
        private readonly List<BonusTextController> levelBoniObjects = new List<BonusTextController>();
        private readonly List<AnyResourcePanel> upgradeCosts = new List<AnyResourcePanel>();

        private void Start()
        {
            dismiss.onClick.AddListener(() => Destroy(gameObject));
        }

        public void SetBuildingType(BuildingType type)
        {
            buildingType = type;
            var building = buildingService.Building(buildingType);
            buildingName.text = StringUtil.Readable(buildingType.ToString());
            levelImg.sprite = generalAtlas.GetSprite($"level_{building.level}");

            var lvl = 2;
            while (propertyService.Properties($"{buildingType}_BUILDING", lvl).IsEmpty() == false)
            {
                var btn = Instantiate(levelButtonPrefab, levelButtonCanvas);
                btn.buildingLevel = lvl;
                btn.SetLevelIcon(generalAtlas.GetSprite($"level_{lvl}"), lvl <= building.level);
                btn.AddClickListener(() =>
                {
                    SetLevel(btn.buildingLevel);
                });
                levelButtons.Add(btn);
                lvl++;
            }

            var maxLvl = lvl - 1;
            if (building.level == maxLvl)
            {
                SetLevel(maxLvl);
            }
            else
            {
                SetLevel(building.level + 1);
            }

            levelButtonCanvas.localPosition = new Vector3((building.level - 1) * -170f, 0, 0);
        }

        public void OnUpgrade(UnityAction call)
        {
            upgradeButton.AddClickListener(call);
        }

        private async void SetLevel(int lvl)
        {
            levelButtons.ForEach(btn =>
            {
                btn.SetActive(btn.buildingLevel == lvl);
            });
            levelTitle.text = $"Level {lvl} rewards";
            levelBoniObjects.ForEach(bonus => Destroy(bonus.gameObject));
            levelBoniObjects.Clear();
            
            propertyService.Properties($"{buildingType}_BUILDING", lvl).ForEach(prop =>
            {
                var bonusText = Instantiate(bonusTextPrefab, levelBoni);
                bonusText.SetText(BonusText(prop));
                levelBoniObjects.Add(bonusText);
            });
            
            upgradeButton.SetInteractable(lvl == (buildingService.Building(buildingType).level + 1));
            upgradeCosts.ForEach(cost => Destroy(cost.gameObject));
            upgradeCosts.Clear();
            propertyService.BuildingUpgradeCosts(buildingType, lvl).ForEach(prop =>
            {
                var cost = Instantiate(resourcePrefab, costsCanvas);
                cost.SetIcon(resourcesAtlas.GetSprite($"{prop.resourceType}"));
                cost.SetAmount(prop.value1);
                upgradeCosts.Add(cost);
            });
            
            await UniTask.Delay(TimeSpan.FromMilliseconds(10));
            var dummyBonus = Instantiate(bonusTextPrefab, levelBoni);
            dummyBonus.SetText("");
            await UniTask.Delay(TimeSpan.FromMilliseconds(10));
            Destroy(dummyBonus.gameObject);
        }

        private string BonusText(Property prop)
        {
            if (prop.resourceType != null)
            {
                return $"+{prop.value1} {StringUtil.Readable(prop.resourceType.ToString())}";
            }

            if (prop.progressStat != null)
            {
                switch (prop.progressStat)
                {
                    case ProgressStat.EXPEDITION_SPEED: return $"+{prop.value1}% Expedition speed";
                    case ProgressStat.NUMBER_ODD_JOBS: return $"+{prop.value1} Odd Job in progress";
                    case ProgressStat.GARAGE_SLOT: return $"+{prop.value1} Garage slot";
                    case ProgressStat.MISSION_SPEED: return $"+{prop.value1}% Mission speed";
                    case ProgressStat.MISSION_MAX_BATTLES: return $"+{prop.value1} Battle{(prop.value1 > 1 ? "s" : "")} per mission";
                    case ProgressStat.BUILDER_QUEUE: return $"+{prop.value1} Builder queue size";
                    case ProgressStat.BUILDER_SPEED: return $"+{prop.value1}% Building upgrade speed";
                    case ProgressStat.BARRACKS_SIZE: return $"+{prop.value1} Barracks space";
                    case ProgressStat.GEAR_QUALITY_INCREASE: return $"+{prop.value1}% increased gear quality";
                    case ProgressStat.HERO_TRAIN_LEVEL: return $"+{prop.value1} Hero train level in the academy";
                    case ProgressStat.TRAINING_XP_BOOST: return $"+{prop.value1}% more XP in the academy";
                    case ProgressStat.TRAINING_ASC_BOOST: return $"+{prop.value1}% more Asc points in the academy";
                    case ProgressStat.VEHICLE_UPGRADE_LEVEL: return $"+{prop.value1} Vehicle upgrade level";
                    case ProgressStat.INCUBATORS: return $"+{prop.value1} Incubator{(prop.value1 > 1 ? "s" : "")}";
                    case ProgressStat.LAB_SPEED: return $"+{prop.value1}% Incubation speed";
                    case ProgressStat.SIMPLE_GENOMES_NEEDED: return $"{prop.value1} Simple genomes needed";
                    case ProgressStat.COMMON_GENOMES_NEEDED: return $"{prop.value1} Common genomes needed";
                    case ProgressStat.UNCOMMON_GENOMES_NEEDED: return $"{prop.value1} Uncommon genomes needed";
                    case ProgressStat.RARE_GENOMES_NEEDED: return $"{prop.value1} Rare genomes needed";
                    case ProgressStat.EPIC_GENOMES_NEEDED: return $"{prop.value1} Epic genomes needed";
                    case ProgressStat.SIMPLE_INCUBATION_UP_PER_MIL: return $"+{NumberUtil.PerMill(prop.value1)}% Chance to clone a common hero using simple genomes";
                    case ProgressStat.COMMON_INCUBATION_UP_PER_MIL: return $"+{NumberUtil.PerMill(prop.value1)}% Chance to clone an uncommon hero using common genomes";
                    case ProgressStat.UNCOMMON_INCUBATION_UP_PER_MIL: return $"+{NumberUtil.PerMill(prop.value1)}% Chance to clone a rare hero using uncommon genomes";
                    case ProgressStat.UNCOMMON_INCUBATION_SUPER_UP_PER_MIL: return $"+{NumberUtil.PerMill(prop.value1)}% Chance to clone an epic hero using uncommon genomes";
                    case ProgressStat.RARE_INCUBATION_UP_PER_MIL: return $"+{NumberUtil.PerMill(prop.value1)}% Chance to clone an epic hero using rare genomes";
                    case ProgressStat.UNCOMMON_STARTING_LEVEL: return $"+{prop.value1} uncommon hero level after incubation";
                    case ProgressStat.JEWEL_UPGRADE_LEVEL: return $"+{prop.value1} Jewel upgrade level";
                    case ProgressStat.JEWEL_MERGE_DOUBLE_CHANCE: return $"+{prop.value1}% Chance to retrieve 2 jewels after merging";
                    case ProgressStat.GEAR_MOD_RARITY: return $"+{prop.value1} Gear rarity allowed to modify";
                    case ProgressStat.GEAR_MOD_SPEED: return $"+{prop.value1}% Gear modification speed";
                    case ProgressStat.GEAR_BREAKDOWN_RARITY: return $"+{prop.value1} Gear rarity allowed to breakdown";
                    case ProgressStat.GEAR_BREAKDOWN_RESOURCES: return $"+{prop.value1}% Resources when breaking down gear";
                    case ProgressStat.REROLL_GEAR_QUALITY: return "New modification: Re roll quality";
                    case ProgressStat.REROLL_GEAR_STAT: return "New modification: Re roll stat";
                    case ProgressStat.INC_GEAR_RARITY: return "New modification: Increase rarity";
                    case ProgressStat.REROLL_GEAR_JEWEL: return "New modification: Re roll jewel slots";
                    case ProgressStat.ADD_GEAR_JEWEL: return "New modification: Add jewel slot";
                    case ProgressStat.ADD_GEAR_SPECIAL_JEWEL: return "New modification: Add set jewel slot";
                    case ProgressStat.NEGOTIATION_LEVEL: return $"+{prop.value1} Negotiation level";
                    case ProgressStat.ENABLE_TRADING: return "Access to trading area granted";
                    case ProgressStat.ENABLE_BLACK_MARKET: return "Access to black market granted";
                    case ProgressStat.ENABLE_CAR_YARD: return "Access to car yard granted";
                    case ProgressStat.MERCHANT_LEVEL: return $"+{prop.value1} Merchant level";
                    default: return $"Unknown stat: {prop.progressStat}";
                }
            }

            return "";
        }
    }
}