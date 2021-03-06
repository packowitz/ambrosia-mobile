using Backend.Models;
using Backend.Models.Enums;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ResourcesService
    {
        public Resources Resources { get; private set; }

        public ResourcesService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.resources != null)
                {
                    Resources = signal.Data.resources;
                    signalBus.Fire(new ResourcesSignal(Resources));
                }
            });
        }
        
        public bool EnoughResources(ResourceType type, int amount) {
            return ResourceAmount(type) >= amount;
        }

        public bool CanClaim(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.STEAM:
                    return Resources.steam < Resources.steamMax;
                case ResourceType.PREMIUM_STEAM:
                    return Resources.premiumSteam < Resources.premiumSteamMax;
                case ResourceType.COGWHEELS:
                    return Resources.cogwheels < Resources.cogwheelsMax;
                case ResourceType.PREMIUM_COGWHEELS:
                    return Resources.premiumCogwheels < Resources.premiumCogwheelsMax;
                case ResourceType.TOKENS:
                    return Resources.tokens < Resources.tokensMax;
                case ResourceType.PREMIUM_TOKENS:
                    return Resources.premiumTokens < Resources.premiumTokensMax;
                case ResourceType.METAL:
                    return Resources.metal < Resources.metalMax;
                case ResourceType.IRON:
                    return Resources.iron < Resources.ironMax;
                case ResourceType.STEEL:
                    return Resources.steel < Resources.steelMax;
                case ResourceType.WOOD:
                    return Resources.wood < Resources.woodMax;
                case ResourceType.BROWN_COAL:
                    return Resources.brownCoal < Resources.brownCoalMax;
                case ResourceType.BLACK_COAL:
                    return Resources.blackCoal < Resources.blackCoalMax;
                default: return true;
            }
        }
        
        public int ResourceAmount(ResourceType type) {
            switch (type)
            {
                case ResourceType.STEAM:
                    return Resources.steam + Resources.premiumSteam;
                case ResourceType.COGWHEELS:
                    return Resources.cogwheels + Resources.premiumCogwheels;
                case ResourceType.TOKENS:
                    return Resources.tokens + Resources.premiumTokens;
                case ResourceType.COINS:
                    return Resources.coins;
                case ResourceType.RUBIES:
                    return Resources.rubies;
                case ResourceType.METAL:
                    return Resources.metal;
                case ResourceType.IRON:
                    return Resources.iron;
                case ResourceType.STEEL:
                    return Resources.steel;
                case ResourceType.WOOD:
                    return Resources.wood;
                case ResourceType.BROWN_COAL:
                    return Resources.brownCoal;
                case ResourceType.BLACK_COAL:
                    return Resources.blackCoal;
                case ResourceType.SIMPLE_GENOME:
                    return Resources.simpleGenome;
                case ResourceType.COMMON_GENOME:
                    return Resources.commonGenome;
                case ResourceType.UNCOMMON_GENOME:
                    return Resources.uncommonGenome;
                case ResourceType.RARE_GENOME:
                    return Resources.rareGenome;
                case ResourceType.EPIC_GENOME:
                    return Resources.epicGenome;
                case ResourceType.WOODEN_KEYS:
                    return Resources.woodenKeys;
                case ResourceType.BRONZE_KEYS:
                    return Resources.bronzeKeys;
                case ResourceType.SILVER_KEYS:
                    return Resources.silverKeys;
                case ResourceType.GOLDEN_KEYS:
                    return Resources.goldenKeys;
                default: return 0;
            }
        }
    }
}