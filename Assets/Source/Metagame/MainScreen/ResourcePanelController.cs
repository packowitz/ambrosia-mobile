using Backend.Services;
using Backend.Signal;
using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Resources = Backend.Models.Resources;

namespace Metagame.MainScreen
{
    public class ResourcePanelController : MonoBehaviour
    {
        [SerializeField] private Button playerAvatar;
        [SerializeField] private GeneratedResourcePanel tokens;
        [SerializeField] private AnyResourcePanel tokensPremium;
        [SerializeField] private GeneratedResourcePanel cogwheels;
        [SerializeField] private AnyResourcePanel cogwheelsPremium;
        [SerializeField] private GeneratedResourcePanel steam;
        [SerializeField] private AnyResourcePanel steamPremium;
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
            UpdateResources(resourcesService.Resources);
            signalBus.Subscribe<ResourcesSignal>(signal =>
            {
                UpdateResources(signal.Data);
            });
        }

        private void UpdateResources(Resources res)
        {
            tokens.SetGeneratedAmount(res.tokens, res.tokensMax, res.tokensProductionTime);
            tokensPremium.SetAmount(res.premiumTokens, res.premiumTokensMax);
            cogwheels.SetGeneratedAmount(res.cogwheels, res.cogwheelsMax, res.cogwheelsProductionTime);
            cogwheelsPremium.SetAmount(res.premiumCogwheels, res.premiumCogwheelsMax);
            steam.SetGeneratedAmount(res.steam, res.steamMax, res.steamProductionTime);
            steamPremium.SetAmount(res.premiumSteam, res.premiumSteamMax);
            coins.SetAmount(res.coins);
            rubies.SetAmount(res.rubies);
        }
    }
}