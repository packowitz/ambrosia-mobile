using Backend;
using Backend.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        [Inject] private HeroBaseService heroBaseService;

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
            else if(!heroBaseService.HeroesInitialized)
            {
                loadingController.SetMessage("Loading Heroes");
                heroBaseService.LoadBaseHeroes();
                await UniTask.WaitUntil(() => heroBaseService.HeroesInitialized);
                Bootstrap().Forget();
            }
            // TODO load properties, VehicleBase
            else
            {
                await SceneManager.LoadSceneAsync("Metagame");
            }
        }
    }
}