using UnityEngine;

namespace Metagame
{
    public class BuildingCanvasController : MonoBehaviour
    {
        public T OpenBuilding<T>(T prefab) where T: MonoBehaviour
        {
            return Instantiate(prefab, transform);
        }
    }
}