using System;
using Backend.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Metagame.MapScreen
{
    public class ChooseMapTextPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void SetMap(PlayerMap map)
        {
            if (map != null)
            {
                if (map.secondsToReset != null)
                {
                    text.text = $"{map.name} (reset in {(map.ResetTime - DateTime.Now).TimerWithUnit()})";
                }
                else
                {
                    text.text = map.name;
                }
                
                if (map.unvisited)
                {
                    text.fontStyle = FontStyles.Bold;
                }
            }
            else
            {
                text.text = "No maps available";
                text.fontStyle = FontStyles.Italic;
            }
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}