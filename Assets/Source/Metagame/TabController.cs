using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private Button tabButton;
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text tabText;
        [SerializeField] private GameObject tabAlert;

        public bool isActive;
        public Color activeColor;
        public Color inactiveColor;
        public string text;
        public bool showAlert;

        private void Start()
        {
            tabText.text = text;
            background.color = isActive ? activeColor : inactiveColor;
            tabAlert.SetActive(showAlert);
        }

        public void AddClickListener(UnityAction call)
        {
            tabButton.onClick.AddListener(call);
        }

        public void ShowAlert(bool show = true)
        {
            showAlert = show;
            tabAlert.SetActive(show);
        }

        public void SetText(string newText)
        {
            text = newText;
            tabText.text = text;
        }

        public void SetActive(bool active = true)
        {
            isActive = active;
            background.color = isActive ? activeColor : inactiveColor;
        }
    }
}