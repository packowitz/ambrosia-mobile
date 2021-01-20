using System;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using Zenject;
using Resources = Backend.Models.Resources;

namespace Metagame.InboxScreen
{
    public class InboxResourcePanelController : MonoBehaviour
    {
        [SerializeField] private AnyResourcePanel tokensPremium;
        [SerializeField] private AnyResourcePanel cogwheelsPremium;
        [SerializeField] private AnyResourcePanel steamPremium;
        [SerializeField] private AnyResourcePanel coins;
        [SerializeField] private AnyResourcePanel rubies;
        [SerializeField] private AnyResourcePanel metal;
        [SerializeField] private AnyResourcePanel iron;
        [SerializeField] private AnyResourcePanel steel;
        [SerializeField] private AnyResourcePanel wood;
        [SerializeField] private AnyResourcePanel brownCoal;
        [SerializeField] private AnyResourcePanel blackCoal;
        
        [Inject] private ResourcesService resourcesService;
        [Inject] private SignalBus signalBus;

        private void Start()
        {
            UpdateResources(resourcesService.Resources);
            signalBus.Subscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<ResourcesSignal>(ConsumeResourcesSignal);
        }

        private void ConsumeResourcesSignal(ResourcesSignal signal)
        {
            UpdateResources(signal.Data);
        }

        private void UpdateResources(Resources res)
        {
            tokensPremium.SetAmount(res.premiumTokens, res.premiumTokens >= res.premiumTokensMax);
            cogwheelsPremium.SetAmount(res.premiumCogwheels, res.premiumCogwheels >= res.premiumCogwheelsMax);
            steamPremium.SetAmount(res.premiumSteam, res.premiumSteam >= res.premiumSteamMax);
            coins.SetAmount(res.coins);
            rubies.SetAmount(res.rubies);
            metal.SetAmount(res.metal, res.metal >= res.metalMax);
            iron.SetAmount(res.iron, res.iron >= res.ironMax);
            steel.SetAmount(res.steel, res.steel >= res.steelMax);
            wood.SetAmount(res.wood, res.wood >= res.woodMax);
            brownCoal.SetAmount(res.brownCoal, res.brownCoal >= res.brownCoalMax);
            blackCoal.SetAmount(res.blackCoal, res.blackCoal >= res.blackCoalMax);
        }
    }
}