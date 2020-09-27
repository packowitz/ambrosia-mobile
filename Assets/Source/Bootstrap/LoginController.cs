using System;
using Backend;
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

        [Inject]
        private PlayerService playerService;

        public event Action PlayerLoggedIn;

        private void Start()
        {
            emailInput.onValueChanged.AddListener(CheckButton);
            passwordInput.onValueChanged.AddListener(CheckButton);
            submitButton.interactable = false;
            submitButton.onClick.AddListener(Login);
        }

        public void Login()
        {
            playerService.Login(emailInput.text, passwordInput.text, data =>
            {
                PlayerLoggedIn?.Invoke();
            });
        }

        private void CheckButton(string value)
        {
            submitButton.interactable =
                !string.IsNullOrEmpty(emailInput.text) && !string.IsNullOrEmpty(passwordInput.text);
        }

        public void RemoveFromView()
        {
            Destroy(gameObject);
        }
    }
}