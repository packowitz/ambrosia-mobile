using System;
using Backend;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            Bootstrap().Forget();
        }

        private async UniTask Bootstrap()
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
                    Bootstrap().Forget();
                };
            }
            else if (!playerService.PlayerInitialized)
            {
                loadingController.SetMessage("Loading Player");
                playerService.LoadPlayer();
                await UniTask.WaitUntil(() => playerService.PlayerInitialized);
                Bootstrap().Forget();
            }
            // TODO load properties, HeroBase, VehicleBase
            else
            {
                await SceneManager.LoadSceneAsync("Metagame");
            }
        }
    }
}