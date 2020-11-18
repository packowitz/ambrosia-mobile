using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private Image loadingIndicator;

        public string text;

        public void Start()
        {
            buttonText.text = text;
            loadingIndicator.enabled = false;
        }

        public void SetInteractable(bool active)
        {
            button.interactable = active;
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void ShowIndicator()
        {
            button.interactable = false;
            buttonText.enabled = false;
            loadingIndicator.enabled = true;
        }
    }
}