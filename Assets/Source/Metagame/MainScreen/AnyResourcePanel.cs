using TMPro;
using UnityEngine;
using Utils;

namespace Metagame.MainScreen
{
    public class AnyResourcePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountText;

        public void SetAmount(int amount)
        {
            amountText.text = NumberUtil.RoundAmount(amount);
        }

        public void SetAmount(int amount, int amountMax)
        {
            amountText.text = $"{NumberUtil.RoundAmount(amount)}/{NumberUtil.RoundAmount(amountMax)}";
        }
    }
}