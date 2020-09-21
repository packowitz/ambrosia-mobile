using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnvConfig", menuName = "Ambrosia/Configs/EnvConfig")]
    public class EnvConfig : ScriptableObject
    {
        [SerializeField] private string apiUrl;
        [SerializeField] private string apiUrlDebug;

#if UNITY_EDITOR
        public string ApiUrl => apiUrlDebug;
#else
        public string ApiUrl => apiUrl;
#endif
    }
}