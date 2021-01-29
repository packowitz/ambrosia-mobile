using TMPro;
using UnityEngine;

namespace Metagame.BuildingsScreen
{
    public class BonusTextController : MonoBehaviour
    {
        [SerializeField] private TMP_Text bonusText;

        public void SetText(string text)
        {
            bonusText.text = text;
        }
    }
}