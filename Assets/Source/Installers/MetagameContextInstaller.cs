using Metagame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MetagameContextInstaller : MonoInstaller
    {
        [SerializeField] private MetagameManager metagameManager;
        
        public override void InstallBindings()
        {
            Container.Bind<MetagameManager>().FromInstance(metagameManager);
        }
    }
}