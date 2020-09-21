using Backend;
using Configs;
using Zenject;

namespace Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ServerAPI>().AsSingle();
            Container.Bind<ConfigsProvider>().AsSingle();
            Container.Bind<PlayerService>().AsSingle();
        }
    }
}