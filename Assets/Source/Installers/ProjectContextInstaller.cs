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
            Container.DeclareSignal<ExpeditionLevelSignal>();
            Container.DeclareSignal<ExpeditionSignal>();
            Container.DeclareSignal<MainScreenChangedSignal>();
            Container.DeclareSignal<MissionSignal>();
            Container.DeclareSignal<PlayerActionSignal>();
            Container.DeclareSignal<ResourcesSignal>();
            
            Container.Bind<ServerAPI>().AsSingle();
            Container.Bind<ConfigsProvider>().AsSingle();
            
            // data services
            Container.Bind<AchievementsService>().AsSingle().NonLazy();
            Container.Bind<ActivityService>().AsSingle().NonLazy();
            Container.Bind<BattleService>().AsSingle().NonLazy();
            Container.Bind<BazaarService>().AsSingle().NonLazy();
            Container.Bind<BuildingService>().AsSingle().NonLazy();
            Container.Bind<ExpeditionService>().AsSingle().NonLazy();
            Container.Bind<ForgeService>().AsSingle().NonLazy();
            Container.Bind<GearService>().AsSingle().NonLazy();
            Container.Bind<HeroBaseService>().AsSingle().NonLazy();
            Container.Bind<HeroService>().AsSingle().NonLazy();
            Container.Bind<InboxService>().AsSingle().NonLazy();
            Container.Bind<IncubatorService>().AsSingle().NonLazy();
            Container.Bind<JewelryService>().AsSingle().NonLazy();
            Container.Bind<LootedService>().AsSingle().NonLazy();
            Container.Bind<MapService>().AsSingle().NonLazy();
            Container.Bind<MissionService>().AsSingle().NonLazy();
            Container.Bind<PlayerService>().AsSingle().NonLazy();
            Container.Bind<ProgressService>().AsSingle().NonLazy();
            Container.Bind<PropertyService>().AsSingle().NonLazy();
            Container.Bind<ResourcesService>().AsSingle().NonLazy();
            Container.Bind<StoryService>().AsSingle().NonLazy();
            Container.Bind<TeamService>().AsSingle().NonLazy();
            Container.Bind<UpgradeService>().AsSingle().NonLazy();
            Container.Bind<VehicleBaseService>().AsSingle().NonLazy();
            Container.Bind<VehicleService>().AsSingle().NonLazy();
        }
    }
}