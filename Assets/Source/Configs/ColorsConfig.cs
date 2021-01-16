using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ColorsConfig", menuName = "Ambrosia/Configs/ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {
        [SerializeField] public Color missionSuccess;
        [SerializeField] public Color missionFailed;
        [SerializeField] public Color missionIdle;
        [SerializeField] public Color expSimple;
        [SerializeField] public Color expSimpleBorder;
        [SerializeField] public Color expCommon;
        [SerializeField] public Color expCommonBorder;
        [SerializeField] public Color expUncommon;
        [SerializeField] public Color expUncommonBorder;
        [SerializeField] public Color expRare;
        [SerializeField] public Color expRareBorder;
        [SerializeField] public Color expEpic;
        [SerializeField] public Color expEpicBorder;
        [SerializeField] public Color expLegendary;
        [SerializeField] public Color expLegendaryBorder;
        [SerializeField] public Color heroAvatarBackground;
        [SerializeField] public Color heroAvatarBackgroundActive;
        [SerializeField] public Color heroLevelGreenBackground;
        [SerializeField] public Color heroLevelBlueBackground;
        [SerializeField] public Color heroLevelRedBackground;
        [SerializeField] public Color heroLevelNeutralBackground;
        [SerializeField] public Color heroMaxLevelBorder;
        [SerializeField] public Color heroAscInnerActive;
        [SerializeField] public Color heroAscInnerInactive;
        [SerializeField] public Color bottomHubActive;
        [SerializeField] public Color bottomHubInactive;
        [SerializeField] public Color buttonSuccess;
        [SerializeField] public Color buttonSuccessInactive;
        [SerializeField] public Color buttonDanger;
        [SerializeField] public Color buttonDangerInactive;
        [SerializeField] public Color buttonSecondary;
        [SerializeField] public Color buttonSecondaryInactive;
        [SerializeField] public Color gearJewel0;
        [SerializeField] public Color gearJewel1;
        [SerializeField] public Color gearJewel2;
        [SerializeField] public Color gearJewel3;
        [SerializeField] public Color gearJewel4;
        [SerializeField] public Color gearSHABBY;
        [SerializeField] public Color gearRUSTY;
        [SerializeField] public Color gearORDINARY;
        [SerializeField] public Color gearUSEFUL;
        [SerializeField] public Color gearGOOD;
        [SerializeField] public Color gearAWESOME;
        [SerializeField] public Color gearFLAWLESS;
        [SerializeField] public Color gearPERFECT;
        [SerializeField] public Color gearGODLIKE;
        [SerializeField] public Color mapFavorite;
        [SerializeField] public Color mapNoFavorite;
    }
}