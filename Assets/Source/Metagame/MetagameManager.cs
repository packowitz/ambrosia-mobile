using Backend.Models.Enums;
using Backend.Signal;
using Metagame.BuildingsScreen;
using UnityEngine;
using Zenject;

namespace Metagame
{
    public class MetagameManager: MonoBehaviour
    {
        [SerializeField] private Canvas mapCanvas;
        [SerializeField] private Canvas inboxCanvas;
        [SerializeField] private Canvas tasksCanvas;
        [SerializeField] private Canvas buildingsCanvas;
        [SerializeField] private BuildingsController buildingsController;
        [SerializeField] private Canvas builderCanvas;
        
        [Inject] private SignalBus signalBus;

        public MainScreenEnum? CurrentScreen;

        private void Start()
        {
            SetMainScreen(MainScreenEnum.Map);
        }

        public void SetMainScreen(MainScreenEnum mainScreen)
        {
            if (mainScreen != CurrentScreen)
            {
                CurrentScreen = mainScreen;
                
                mapCanvas.enabled = CurrentScreen == MainScreenEnum.Map;
                inboxCanvas.enabled = CurrentScreen == MainScreenEnum.Inbox;
                tasksCanvas.enabled = CurrentScreen == MainScreenEnum.Tasks;
                buildingsCanvas.enabled = CurrentScreen == MainScreenEnum.Buildings;
                builderCanvas.enabled = CurrentScreen == MainScreenEnum.Builder;
                
                signalBus.Fire<MainScreenChangedSignal>();
            }
        }

        public void OpenBuilding(BuildingType buildingType)
        {
            SetMainScreen(MainScreenEnum.Buildings);
            buildingsController.OpenBuilding(buildingType);
        }

        public void OpenBuildingUpgrade(BuildingType buildingType)
        {
            SetMainScreen(MainScreenEnum.Builder);
            // TODO set upgrade to buildingType
        }
    }
}