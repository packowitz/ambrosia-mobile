using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using UnityEngine.U2D;
using Zenject;
using Resources = Backend.Models.Resources;

namespace Metagame.MapScreen
{
    public class StartFightResourcePanelController : MonoBehaviour
    {
        [SerializeField] private AnyResourcePanel coins;
        [SerializeField] private AnyResourcePanel rubies;
        [SerializeField] private AnyResourcePanel premiumResource;
        [SerializeField] private GeneratedResourcePanel timeBasedResource;
        [SerializeField] private SpriteAtlas resourceAtlas;
        
        [Inject] private ResourcesService resourcesService;
        [Inject] private SignalBus signalBus;

        private ResourceType resourceType = ResourceType.STEAM;
        
        private void Start()
        {
            UpdateResources(resourcesService.Resources);
            signalBus.Subscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void ConsumeResourcesSignal(ResourcesSignal signal)
        {
            UpdateResources(signal.Data);
        }

        public void SetResourceType(ResourceType resType)
        {
            resourceType = resType;
            UpdateResources(resourcesService.Resources);
        }
        
        private void UpdateResources(Resources res)
        {
            coins.SetAmount(res.coins);
            rubies.SetAmount(res.rubies);
            switch (resourceType)
            {
                case ResourceType.STEAM:
                    premiumResource.SetAmount(res.premiumSteam, res.premiumSteamMax);
                    premiumResource.SetIcon(resourceAtlas.GetSprite("PREMIUM_STEAM"));
                    timeBasedResource.SetGeneratedAmount(res.steam, res.steamMax, res.SteamProductionTime);
                    timeBasedResource.SetIcon(resourceAtlas.GetSprite("STEAM"));
                    break;
                case ResourceType.COGWHEELS:
                    premiumResource.SetAmount(res.premiumCogwheels, res.premiumCogwheelsMax);
                    premiumResource.SetIcon(resourceAtlas.GetSprite("PREMIUM_COGWHEELS"));
                    timeBasedResource.SetGeneratedAmount(res.cogwheels, res.cogwheelsMax, res.CogwheelsProductionTime);
                    timeBasedResource.SetIcon(resourceAtlas.GetSprite("COGWHEELS"));
                    break;
                case ResourceType.TOKENS:
                    premiumResource.SetAmount(res.premiumTokens, res.premiumTokensMax);
                    premiumResource.SetIcon(resourceAtlas.GetSprite("PREMIUM_TOKENS"));
                    timeBasedResource.SetGeneratedAmount(res.tokens, res.tokensMax, res.TokensProductionTime);
                    timeBasedResource.SetIcon(resourceAtlas.GetSprite("TOKENS"));
                    break;
            }
        }
    }
}