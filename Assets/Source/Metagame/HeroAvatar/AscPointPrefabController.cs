using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.HeroAvatar
{
    public class AscPointPrefabController : MonoBehaviour
    {
        [SerializeField] private Image inner;
        [SerializeField] private Image outer;
        
        [Inject] private ConfigsProvider configsProvider;

        public void SetActive(bool active)
        {
            var colorsConfig = configsProvider.Get<ColorsConfig>();
            outer.color = colorsConfig.heroAscInnerInactive;
            inner.color = active ? colorsConfig.heroAscInnerActive : colorsConfig.heroAscInnerInactive;
        }
    }
}