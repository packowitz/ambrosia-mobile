using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame
{
    public class ButtonResourceController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private Image resourceIcon;
        [SerializeField] private TMP_Text resourceAmount;
        [SerializeField] private Image loadingIndicator;

        public string text;

        public void Start()
        {
            buttonText.text = text;
            loadingIndicator.gameObject.SetActive(false);
        }

        public void SetText(string txt)
        {
            text = txt;
            buttonText.text = txt;
        }

        public void SetResource(Sprite icon, string amountTxt)
        {
            resourceIcon.sprite = icon;
            resourceAmount.text = amountTxt;
        }

        public void SetResourceIcon(Sprite icon)
        {
            resourceIcon.sprite = icon;
        }

        public void SetResourceAmount(string amountTxt)
        {
            resourceAmount.text = amountTxt;
        }

        public void SetColor(Color color, Color? inactive = null)
        {
            var colors = button.colors;
            colors.normalColor = color;
            if (inactive != null)
            {
                colors.disabledColor = (Color) inactive;
            }
            button.colors = colors;
        }

        public void SetInteractable(bool active)
        {
            button.interactable = active;
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }

        public void ShowIndicator(bool show = true)
        {
            button.interactable = !show;
            buttonText.enabled = !show;
            loadingIndicator.gameObject.SetActive(show);
        }
    }
}