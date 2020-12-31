using System;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Models.Enums.ObjectShape;
using Backend.Services;
using Metagame.HeroAvatar;
using Metagame.VehicleAvatar;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Metagame
{
    public class LootItemPrefabController : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text centerText;
        [SerializeField] private TMP_Text bottomText;
        [SerializeField] private RectTransform dynamicContainer;
        [SerializeField] private SpriteAtlas resourcesAtlas;
        [SerializeField] private SpriteAtlas progressAtlas;
        [SerializeField] private SpriteAtlas jewelAtlas;
        [SerializeField] private SpriteAtlas vehicleAtlas;
        [SerializeField] private GearAvatarController gearAvatarPrefab;
        [SerializeField] private HeroAvatarPrefabController heroAvatarPrefab;
        [SerializeField] private VehicleAvatarPrefabController vehicleAvatarPrefab;

        [Inject] private GearService gearService;
        [Inject] private HeroService heroService;
        [Inject] private VehicleService vehicleService;

        public void SetItem(LootedItem item, float adjustToHeight)
        {
            switch (item.type)
            {
                case LootedItemType.RESOURCE:
                    SetResource(item.resourceType, item.value, adjustToHeight);
                    break;
                case LootedItemType.PROGRESS:
                    SetProgress(item.progressStat, item.value, adjustToHeight);
                    break;
                case LootedItemType.JEWEL:
                    SetJewel(item.jewelType, Convert.ToInt32(item.value), adjustToHeight);
                    break;
                case LootedItemType.HERO:
                    SetHero(heroService.Hero(item.value), adjustToHeight);
                    break;
                case LootedItemType.GEAR:
                    SetGear(gearService.Gear(item.value), adjustToHeight);
                    break;
                case LootedItemType.VEHICLE:
                    SetVehicle(vehicleService.Vehicle(item.value), adjustToHeight);
                    break;
                case LootedItemType.VEHICLE_PART:
                    SetVehiclePart(vehicleService.VehiclePart(item.value), adjustToHeight);
                    break;
            }
        }

        public void SetResource(ResourceType type, long resourceAmount, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            bottomText.gameObject.SetActive(false);
            icon.sprite = resourcesAtlas.GetSprite(type.ToString());
            centerText.text = $"{NumberUtil.RoundAmount(resourceAmount)}";
        }

        public void SetProgress(ProgressStat stat, long increase, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            bottomText.gameObject.SetActive(false);
            icon.sprite = progressAtlas.GetSprite(stat.ToString());
            var amountText = (increase > 0 ? "+" : "") + $"{NumberUtil.RoundAmount(increase)}";
            switch (stat)
            {
                case ProgressStat.BUILDER_SPEED:
                case ProgressStat.LAB_SPEED:
                case ProgressStat.MISSION_SPEED:
                case ProgressStat.EXPEDITION_SPEED:
                case ProgressStat.GEAR_MOD_SPEED:
                case ProgressStat.GEAR_BREAKDOWN_RESOURCES:
                case ProgressStat.GEAR_QUALITY_INCREASE:
                case ProgressStat.BATTLE_XP_BOOST:
                case ProgressStat.TRAINING_XP_BOOST:
                case ProgressStat.TRAINING_ASC_BOOST:
                case ProgressStat.JEWEL_MERGE_DOUBLE_CHANCE:
                    amountText += "%";
                    break;
                case ProgressStat.PLAYER_XP:
                    amountText = $"{NumberUtil.RoundAmount(increase)}";
                    break;
                case ProgressStat.SIMPLE_INCUBATION_UP_PER_MIL:
                case ProgressStat.COMMON_INCUBATION_UP_PER_MIL:
                case ProgressStat.UNCOMMON_INCUBATION_UP_PER_MIL:
                case ProgressStat.UNCOMMON_INCUBATION_SUPER_UP_PER_MIL:
                case ProgressStat.RARE_INCUBATION_UP_PER_MIL:
                    amountText = $"+{Convert.ToDouble(increase) / 10}%";
                    break;
                case ProgressStat.AUTO_BREAKDOWN_ENABLED:
                    amountText = "";
                    break;
            }
            centerText.text = amountText;
        }

        public void SetJewel(JewelTypeObj type, int level, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            centerText.gameObject.SetActive(false);
            icon.sprite = jewelAtlas.GetSprite($"{type.slot.ToString()}_{level}");
            switch (type.name)
            {
                case JewelType.HP_ABS:
                    bottomText.text = $"L{level} HP";
                    break;
                case JewelType.HP_PERC:
                    bottomText.text = $"L{level} HP %";
                    break;
                case JewelType.ARMOR_ABS:
                    bottomText.text = $"L{level} Arm";
                    break;
                case JewelType.ARMOR_PERC:
                    bottomText.text = $"L{level} Arm %";
                    break;
                case JewelType.STRENGTH_ABS:
                    bottomText.text = $"L{level} Str";
                    break;
                case JewelType.STRENGTH_PERC:
                    bottomText.text = $"L{level} Str %";
                    break;
                case JewelType.CRIT:
                    bottomText.text = $"L{level} Crit";
                    break;
                case JewelType.CRIT_MULT:
                    bottomText.text = $"L{level} CritM";
                    break;
                case JewelType.RESISTANCE:
                    bottomText.text = $"L{level} Res";
                    break;
                case JewelType.DEXTERITY:
                    bottomText.text = $"L{level} Dex";
                    break;
                case JewelType.INITIATIVE:
                    bottomText.text = $"L{level} Ini";
                    break;
                case JewelType.SPEED:
                    bottomText.text = $"L{level} Speed";
                    break;
                case JewelType.STONE_SKIN:
                    bottomText.text = $"L{level} StSk";
                    break;
                case JewelType.VITAL_AURA:
                    bottomText.text = $"L{level} ViAu";
                    break;
                case JewelType.POWER_FIST:
                    bottomText.text = $"L{level} PoFi";
                    break;
                case JewelType.BUFFS_BLESSING:
                    bottomText.text = $"L{level} BuBl";
                    break;
                case JewelType.BERSERKERS_AXE:
                    bottomText.text = $"L{level} BeAx";
                    break;
                case JewelType.MYTHICAL_MIRROR:
                    bottomText.text = $"L{level} MyMi";
                    break;
                case JewelType.WARHORN:
                    bottomText.text = $"L{level} Warh";
                    break;
                case JewelType.REVERSED_REALITY:
                    bottomText.text = $"L{level} ReRe";
                    break;
                case JewelType.TERRIBLE_FATE:
                    bottomText.text = $"L{level} TeFa";
                    break;
            }
        }

        public void SetGear(Gear gear, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            icon.gameObject.SetActive(false);
            centerText.gameObject.SetActive(false);
            bottomText.gameObject.SetActive(false);
            var prefab = Instantiate(gearAvatarPrefab, dynamicContainer);
            prefab.SetGear(gear, dynamicContainer.rect.height);
            prefab.ShowBreakdownLayer(gear.markedToBreakdown);
            prefab.AddClickListener(() =>
            {
                gear.markedToBreakdown = !gear.markedToBreakdown;
                prefab.ShowBreakdownLayer(gear.markedToBreakdown);
            });
        }

        public void SetHero(Hero hero, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            icon.gameObject.SetActive(false);
            centerText.gameObject.SetActive(false);
            bottomText.gameObject.SetActive(false);
            var prefab = Instantiate(heroAvatarPrefab, dynamicContainer);
            prefab.SetHero(hero, dynamicContainer.rect.height);
        }

        public void SetVehicle(Vehicle vehicle, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            icon.gameObject.SetActive(false);
            centerText.gameObject.SetActive(false);
            bottomText.gameObject.SetActive(false);
            var prefab = Instantiate(vehicleAvatarPrefab, dynamicContainer);
            prefab.SetVehicle(vehicle, dynamicContainer.rect.height);
        }

        public void SetVehiclePart(VehiclePart part, float adjustToHeight)
        {
            dynamicContainer.ScaleToHeight(adjustToHeight);
            centerText.gameObject.SetActive(false);
            bottomText.gameObject.SetActive(false);
            icon.sprite = vehicleAtlas.GetSprite(part.type.ToString());
        }
    }
}