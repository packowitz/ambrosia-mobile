using TMPro;
using UnityEditor.U2D;
using UnityEngine;

namespace Bootstrap
{
    public class LoadingController: MonoBehaviour
    {
        [SerializeField]
        private TMP_Text messageField;

        public void SetMessage(string message)
        {
            messageField.text = message;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}