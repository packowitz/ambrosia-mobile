using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame.MapScreen
{
    public class ChooseMapTextPrefab : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public void SetText(string mapName, bool italic = false)
        {
            text.text = mapName;
            if (italic)
            {
                text.fontStyle = FontStyles.Italic;
            }
        }
        
        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}