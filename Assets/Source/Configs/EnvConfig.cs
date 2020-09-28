using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnvConfig", menuName = "Ambrosia/Configs/EnvConfig")]
    public class EnvConfig : ScriptableObject
    {
        [SerializeField] private string apiUrl;
        [SerializeField] private string apiUrlDebug;
        [SerializeField] private bool useLocal;

#if UNITY_EDITOR
        public string ApiUrl => useLocal ? apiUrlDebug : apiUrl;
        public bool IsLocal => useLocal;
#else
        public string ApiUrl => apiUrl;
        public bool IsLocal => false;
#endif
    }
}