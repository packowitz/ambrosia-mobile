using Backend.Signal;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Metagame
{
    public class MetagameManager: MonoBehaviour
    {
        [SerializeField] private Canvas mapCanvas;
        [SerializeField] private Canvas inboxCanvas;
        
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
                Debug.Log($"Changing main screen to {mainScreen}");
                CurrentScreen = mainScreen;
                mapCanvas.enabled = CurrentScreen == MainScreenEnum.Map;
                inboxCanvas.enabled = CurrentScreen == MainScreenEnum.Inbox;
                
                signalBus.Fire<MainScreenChangedSignal>();
            }
        }
    }
}