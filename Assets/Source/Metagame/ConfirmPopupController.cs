using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame
{
    public class ConfirmPopupController : MonoBehaviour
    {
        [SerializeField] private Button dismiss;
        [SerializeField] private ButtonController cancel;
        [SerializeField] private TMP_Text description;
        [SerializeField] private ButtonController confirmButton;

        private bool loading;

        private void Start()
        {
            dismiss.onClick.AddListener(ClosePopup);
            cancel.AddClickListener(ClosePopup);
        }

        public void OnConfirm(UnityAction call)
        {
            confirmButton.AddClickListener(call);
        }

        public void ShowIndicator()
        {
            loading = true;
            confirmButton.ShowIndicator();
        }

        public void SetText(string descTxt)
        {
            description.text = descTxt;
        }

        public void ClosePopup()
        {
            if (!loading)
            {
                Destroy(gameObject);
            }
        }
    }
    
}