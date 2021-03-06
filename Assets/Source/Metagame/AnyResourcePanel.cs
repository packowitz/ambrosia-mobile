using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Metagame
{
    public class AnyResourcePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private Image icon;

        public void SetAmount(int amount)
        {
            amountText.text = NumberUtil.RoundAmount(amount);
        }

        public void SetAmount(int amount, int amountMax)
        {
            amountText.text = $"{NumberUtil.RoundAmount(amount)}/{NumberUtil.RoundAmount(amountMax)}";
        }

        public void SetAmount(int amount, bool max)
        {
            SetAmount(amount);
            amountText.fontStyle = max ? FontStyles.Bold : FontStyles.Normal;
        }

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}