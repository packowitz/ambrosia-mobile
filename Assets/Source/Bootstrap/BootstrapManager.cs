using System;
using Backend;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Bootstrap
{
    public class BootstrapManager : MonoBehaviour
    {
        [SerializeField] private LoadingController loadingController;

        [SerializeField] private LoginController loginPrefab;

        [SerializeField] private RectTransform canvas;

        [Inject] private ServerAPI serverAPI;

        [Inject] private PlayerService playerService;

        private void Start()
        {
            Bootstrap();
        }

        private void Bootstrap()
        {
            if (!serverAPI.IsLoggedIn)
            {
                // goto login
                loadingController.SetActive(false);
                var loginController = Instantiate(loginPrefab, canvas);
                loginController.PlayerLoggedIn += () =>
                {
                    loginController.RemoveFromView();
                    loadingController.SetActive(true);
                    Bootstrap();
                };
            }
            else if (!playerService.PlayerInitialized)
            {
                loadingController.SetMessage("Loading Player");
                playerService.LoadPlayer();
            }
        }
    }
}