using Backend;
using Backend.Services;
using Backend.Signal;
using Configs;
using Zenject;

namespace Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // enable signals
            SignalBusInstaller.Install(Container);
            
            // declare signals
            Container.DeclareSignal<PlayerActionSignal>();
            
            Container.Bind<ServerAPI>().AsSingle();
            Container.Bind<ConfigsProvider>().AsSingle();
            
            // services
            Container.Bind<AchievementsService>().AsSingle();
            Container.Bind<BuildingService>().AsSingle();
            Container.Bind<GearService>().AsSingle();
            Container.Bind<HeroBaseService>().AsSingle();
            Container.Bind<HeroService>().AsSingle();
            Container.Bind<JewelryService>().AsSingle();
            Container.Bind<PlayerService>().AsSingle();
            Container.Bind<ProgressService>().AsSingle();
            Container.Bind<PropertyService>().AsSingle();
            Container.Bind<ResourcesService>().AsSingle();
            Container.Bind<VehicleBaseService>().AsSingle();
        }
    }
}