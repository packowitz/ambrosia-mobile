using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Metagame.MainScreen
{
    public class GeneratedResourcePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text generatedAmountText;
        [SerializeField] private Button toggleBtn;

        private bool showAmount = true;
        private int genAmount;
        private int genAmountMax;
        private DateTime nextGen;

        private void Start()
        {
            toggleBtn.onClick.AddListener(() =>
            {
                showAmount = !showAmount;
                UpdateGeneratedText();
            });
        }

        private void Update()
        {
            if (!showAmount)
            {
                UpdateGeneratedText();
            }
        }

        public void SetGeneratedAmount(int amount, int max, DateTime nextGen)
        {

            genAmount = amount;
            genAmountMax = max;
            this.nextGen = nextGen;
            UpdateGeneratedText();
        }

        private void UpdateGeneratedText()
        {
            if (showAmount)
            {
                generatedAmountText.text = $"{genAmount}/{genAmountMax}";
            }
            else
            {
                if (genAmount >= genAmountMax)
                {
                    generatedAmountText.text = "max";
                }
                else
                {
                    var timeLeft = nextGen - DateTime.Now;
                    generatedAmountText.text = timeLeft.Timer();
                }
            }
        }
    }
}