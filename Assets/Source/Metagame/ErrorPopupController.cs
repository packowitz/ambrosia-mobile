using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metagame
{
    public class ErrorPopupController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private ButtonController actionButton;

        private void Start()
        {
            dismiss.onClick.AddListener(() => Destroy(gameObject));
            actionButton.AddClickListener(() => Destroy(gameObject));
        }

        public void SetError(string titleTxt, string descTxt)
        {
            title.text = titleTxt;
            description.text = descTxt;
        }
    }
    
}