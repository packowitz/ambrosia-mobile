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
        [Inject] private HeroBaseService heroBaseService;
        [Inject] private PlayerService playerService;
        [Inject] private PropertyService propertyService;
        [Inject] private VehicleBaseService vehicleBaseService;

        private void Start()
        {
            Bootstrap();
        }

        private async void Bootstrap()
        {
            if (!serverAPI.IsLoggedIn)
            {
                loadingController.SetActive(false);
                Instantiate(loginPrefab, canvas);
                await UniTask.WaitUntil(() => serverAPI.IsLoggedIn);
                loadingController.SetActive(true);
            }
            if (!playerService.PlayerInitialized)
            {
                loadingController.SetMessage("Loading Player");
                playerService.LoadPlayer();
                await UniTask.WaitUntil(() => playerService.PlayerInitialized);
            }
            if (!heroBaseService.HeroesInitialized)
            {
                loadingController.SetMessage("Loading Heroes");
                heroBaseService.LoadBaseHeroes();
                await UniTask.WaitUntil(() => heroBaseService.HeroesInitialized);
            }
            if (!vehicleBaseService.VehiclesInitialized)
            {
                loadingController.SetMessage("Loading Vehicles");
                vehicleBaseService.LoadBaseVehicles();
                await UniTask.WaitUntil(() => vehicleBaseService.VehiclesInitialized);
            }

            if (!propertyService.IsInitialized)
            {
                loadingController.SetMessage("Loading Properties");
                propertyService.CheckVersions();
                await UniTask.WaitUntil(() => propertyService.IsInitialized);
            }
            
            await SceneManager.LoadSceneAsync("Metagame");
        }
    }
}