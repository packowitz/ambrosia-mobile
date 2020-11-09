using UnityEngine;

namespace Metagame
{
    public class PopupCanvasController : MonoBehaviour
    {
        public T OpenPopup<T>(T prefab) where T: MonoBehaviour
        {
            return Instantiate(prefab, transform);
        }
    }
}