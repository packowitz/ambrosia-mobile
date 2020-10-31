using TMPro;
using UnityEngine;

namespace Metagame.MainScreen
{
    public class AnyResourcePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountText;

        public void SetAmount(int amount)
        {
            amountText.text = amount.ToString();
        }

        public void SetAmount(int amount, int amountMax)
        {
            amountText.text = $"{amount}/{amountMax}";
        }
    }
}