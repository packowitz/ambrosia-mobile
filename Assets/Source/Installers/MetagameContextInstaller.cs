using Metagame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MetagameContextInstaller : MonoInstaller
    {
        [SerializeField] private MetagameManager metagameManager;
        [SerializeField] private PopupCanvasController popupCanvasController;
        [SerializeField] private BuildingCanvasController buildingCanvasController;
        
        public override void InstallBindings()
        {
            Container.Bind<MetagameManager>().FromInstance(metagameManager);
            Container.Bind<PopupCanvasController>().FromInstance(popupCanvasController);
            Container.Bind<BuildingCanvasController>().FromInstance(buildingCanvasController);
        }
    }
}