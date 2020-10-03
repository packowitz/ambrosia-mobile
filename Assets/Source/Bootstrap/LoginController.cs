using System;
using Backend.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Bootstrap
{
    public class LoginController: MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField emailInput;
        
        [SerializeField]
        private TMP_InputField passwordInput;

        [SerializeField]
        private Button submitButton;

        [SerializeField]
        private Button showRegisterButton;

        [Inject]
        private PlayerService playerService;
        
        public event Action ShowRegisterButtonPressed;

        private void Start()
        {
            emailInput.onValueChanged.AddListener(CheckButton);
            passwordInput.onValueChanged.AddListener(CheckButton);
            submitButton.interactable = false;
            submitButton.onClick.AddListener(Login);
            showRegisterButton.onClick.AddListener(GotoRegister);
        }

        public void Login()
        {
            playerService.Login(emailInput.text, passwordInput.text, data =>
            {
                Destroy(gameObject);
            });
        }

        private void CheckButton(string value)
        {
            submitButton.interactable =
                !string.IsNullOrEmpty(emailInput.text) && !string.IsNullOrEmpty(passwordInput.text);
        }

        private void GotoRegister()
        {
            ShowRegisterButtonPressed?.Invoke();
            Destroy(gameObject);
        }
    }
}