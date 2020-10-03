using System;
using Backend.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Bootstrap
{
    public class RegisterController: MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inGameNameInput;
        
        [SerializeField]
        private TMP_InputField emailInput;
        
        [SerializeField]
        private TMP_InputField passwordInput;
        
        [SerializeField]
        private TMP_InputField rePasswordInput;

        [SerializeField]
        private Button submitButton;

        [SerializeField]
        private Button showLoginButton;

        [Inject]
        private PlayerService playerService;

        public event Action ShowLoginButtonPressed;

        private void Start()
        {
            inGameNameInput.onValueChanged.AddListener(CheckButton);
            emailInput.onValueChanged.AddListener(CheckButton);
            passwordInput.onValueChanged.AddListener(CheckButton);
            rePasswordInput.onValueChanged.AddListener(CheckButton);
            submitButton.interactable = false;
            submitButton.onClick.AddListener(Register);
            showLoginButton.onClick.AddListener(GotoLogin);
        }

        public void Register()
        {
            playerService.Register(inGameNameInput.text, emailInput.text, passwordInput.text, data =>
            {
                Destroy(gameObject);
            });
        }

        private void CheckButton(string value)
        {
            submitButton.interactable =
                !string.IsNullOrEmpty(inGameNameInput.text) &&
                rePasswordInput.text == passwordInput.text && 
                !string.IsNullOrEmpty(emailInput.text) && 
                !string.IsNullOrEmpty(passwordInput.text);
        }

        private void GotoLogin()
        {
            ShowLoginButtonPressed?.Invoke();
            Destroy(gameObject);
        }
    }
}