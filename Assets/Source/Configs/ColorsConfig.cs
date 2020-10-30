using UnityEngine;
using Color = Backend.Models.Color;

namespace Configs
{
    [CreateAssetMenu(fileName = "ColorsConfig", menuName = "Ambrosia/Configs/ColorsConfig")]
    public class ColorsConfig : ScriptableObject
    {
        [SerializeField] public UnityEngine.Color missionSuccess;
        [SerializeField] public UnityEngine.Color missionFailed;
        [SerializeField] public UnityEngine.Color missionIdle;
        [SerializeField] public UnityEngine.Color expSimple;
        [SerializeField] public UnityEngine.Color expCommon;
        [SerializeField] public UnityEngine.Color expUncommon;
        [SerializeField] public UnityEngine.Color expRare;
        [SerializeField] public UnityEngine.Color expEpic;
        [SerializeField] public UnityEngine.Color expLegendary;
    }
}