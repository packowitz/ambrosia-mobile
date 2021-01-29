using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame.BuildingsScreen
{
    public class BuildingLevelButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image levelImg;
        [SerializeField] private Image activeImg;

        public Color levelPassedColor;
        public Color activeColor;

        public int buildingLevel;

        public void SetLevelIcon(Sprite icon, bool levelPassed)
        {
            levelImg.sprite = icon;
            levelImg.color = levelPassed ? levelPassedColor : activeColor;
        }

        public void SetActive(bool active)
        {
            activeImg.gameObject.SetActive(active);
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}