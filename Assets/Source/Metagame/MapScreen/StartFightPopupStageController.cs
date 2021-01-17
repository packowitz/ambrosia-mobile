using Backend.Models;
using Metagame.HeroAvatar;
using TMPro;
using UnityEngine;

namespace Metagame.MapScreen
{
    public class StartFightPopupStageController : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private HeroAvatarPrefabController opp1;
        [SerializeField] private HeroAvatarPrefabController opp2;
        [SerializeField] private HeroAvatarPrefabController opp3;
        [SerializeField] private HeroAvatarPrefabController opp4;

        public void SetStage(FightStageResolved stage)
        {
            title.text = $"Stage {stage.stage}";
            opp1.SetHero(stage.hero1);
            opp2.SetHero(stage.hero2);
            opp3.SetHero(stage.hero3);
            opp4.SetHero(stage.hero4);
        }
    }
    
}