using Backend.Services;
using Backend.Signal;
using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Metagame.MainScreen
{
    public class ResourcePanelController : MonoBehaviour
    {
        [SerializeField] private Button playerAvatar;
        [SerializeField] private GeneratedResourcePanel tokens;
        [SerializeField] private GeneratedResourcePanel cogwheels;
        [SerializeField] private GeneratedResourcePanel steam;
        [SerializeField] private AnyResourcePanel coins;
        [SerializeField] private AnyResourcePanel rubies;
        [Inject] private PlayerService playerService;
        [Inject] private ResourcesService resourcesService;

        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        private void Start()
        {
            var colorConfig = configsProvider.Get<CharColorsConfig>().GetConfig(playerService.Player.color);
            playerAvatar.image.color = colorConfig.playerBorderColor;
            UpdateResources();
            signalBus.Subscribe<ResourcesSignal>(UpdateResources);
        }

        private void UpdateResources()
        {
            var res = resourcesService.Resources;
            tokens.SetGeneratedAmount(res.tokens, res.tokensMax, res.tokensProductionTime);
            tokens.SetPremiumAmount(res.premiumTokens, res.premiumTokensMax);
            cogwheels.SetGeneratedAmount(res.cogwheels, res.cogwheelsMax, res.cogwheelsProductionTime);
            cogwheels.SetPremiumAmount(res.premiumCogwheels, res.premiumCogwheelsMax);
            steam.SetGeneratedAmount(res.steam, res.steamMax, res.steamProductionTime);
            steam.SetPremiumAmount(res.premiumSteam, res.premiumSteamMax);
            coins.SetAmount(res.coins);
            rubies.SetAmount(res.rubies);
        }
    }
}