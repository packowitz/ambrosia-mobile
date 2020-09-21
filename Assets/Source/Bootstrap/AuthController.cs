using Backend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Bootstrap
{
    public class AuthController: MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField emailInput;
        
        [SerializeField]
        private TMP_InputField passwordInput;

        [SerializeField]
        private Button submitButton;

        [Inject]
        private PlayerService playerService;

        private void Start()
        {
            emailInput.onValueChanged.AddListener(CheckButton);
            passwordInput.onValueChanged.AddListener(CheckButton);
            submitButton.interactable = false;
            submitButton.onClick.AddListener(Login);
        }

        public void Login()
        {
            playerService.Login(emailInput.text, passwordInput.text);
        }

        private void CheckButton(string value)
        {
            submitButton.interactable =
                !string.IsNullOrEmpty(emailInput.text) && !string.IsNullOrEmpty(passwordInput.text);
        }
    }
}