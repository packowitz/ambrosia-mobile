using System;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utils;
using Zenject;
using Color = Backend.Models.Enums.Color;

namespace Metagame.HeroAvatar
{
    public class HeroAvatarPrefabController: MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Image avatarImg;
        [SerializeField] private Image avatarBackground;
        [SerializeField] private TMP_Text level;
        [SerializeField] private Image levelBorder;
        [SerializeField] private Image levelInner;
        [SerializeField] private Image starImage;
        [SerializeField] private RectTransform ascPointsContainer;
        [SerializeField] private AscPointPrefabController ascPointPrefab;
        [SerializeField] private HorizontalLayoutGroup ascLayoutGroup;
        [SerializeField] private SpriteAtlas heroAtlas;
        [SerializeField] private SpriteAtlas generalAtlas;
        [SerializeField] private Button button;
        [SerializeField] private GameObject avatar;
        [SerializeField] private GameObject availableLayer;
        
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private HeroBaseService heroBaseService;

        public void SetHero(Hero hero, float? adjustToHeight = null, bool indicateAvailability = false)
        {
            canvas.ScaleToHeight(adjustToHeight);
            if (hero == null)
            {
                avatar.SetActive(false);
                return;
            }
            avatar.SetActive(true);
            availableLayer.SetActive(indicateAvailability && !hero.IsAvailable());
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            var baseHero = heroBaseService.GetHeroBase(hero.heroBaseId);
            avatarImg.sprite = heroAtlas.GetSprite(hero.avatar);
            starImage.sprite = generalAtlas.GetSprite($"star_{baseHero.rarity.Stars()}_{hero.stars}");
            level.text = hero.level.ToString();
            if (hero.xp == hero.maxXp && hero.level % 10 == 0)
            {
                levelBorder.color = colorsConfig.heroMaxLevelBorder;
            }

            switch (hero.color)
            {
                case Color.NEUTRAL:
                    levelInner.color = colorsConfig.heroLevelNeutralBackground;
                    break;
                case Color.RED:
                    levelInner.color = colorsConfig.heroLevelRedBackground;
                    break;
                case Color.GREEN:
                    levelInner.color = colorsConfig.heroLevelGreenBackground;
                    break;
                case Color.BLUE:
                    levelInner.color = colorsConfig.heroLevelBlueBackground;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var i = 1; i <= baseHero.maxAscLevel; i++)
            {
                var ascPrefab = Instantiate(ascPointPrefab, ascPointsContainer);
                ascPrefab.SetActive(hero.ascLvl >= i);
            }

            if (baseHero.maxAscLevel == 8)
            {
                ascLayoutGroup.spacing = 3;
            }
            
            SetActive(false);
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void SetActive(bool active)
        {
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            avatarBackground.color = active ? colorsConfig.heroAvatarBackgroundActive : colorsConfig.heroAvatarBackground;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}