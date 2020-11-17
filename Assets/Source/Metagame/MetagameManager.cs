using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Metagame
{
    public class MetagameManager: MonoBehaviour
    {
        [SerializeField] private GameObject mapPanel;
        [SerializeField] private GameObject inboxPanel;
        
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
                mapPanel.SetActive(CurrentScreen == MainScreenEnum.Map);
                inboxPanel.SetActive(CurrentScreen == MainScreenEnum.Inbox);
                
                signalBus.Fire<MainScreenChangedSignal>();
            }
        }
    }
}