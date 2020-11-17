using System.Collections.Generic;
using Backend.Services;
using Backend.Signal;
using UnityEngine;
using Zenject;

namespace Metagame.MapScreen
{
    public class AvailableExpeditionsController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas;
        [SerializeField] private AvailableExpeditionPrefabController availableExpeditionPrefab;
        [Inject] private ExpeditionService expeditionService;
        [Inject] private SignalBus signalBus;
        
        private List<AvailableExpeditionPrefabController> expeditions = new List<AvailableExpeditionPrefabController>();
        
        private void Start()
        {
            UpdateExpeditions();
            signalBus.Subscribe<ExpeditionSignal>(UpdateExpeditions);
        }

        private void UpdateExpeditions()
        {
            expeditions.ForEach(e => e.Remove());
            expeditions.Clear();
            expeditionService.AvailableExpeditions().ForEach(expedition =>
            {
                var expeditionOverlay = Instantiate(availableExpeditionPrefab, canvas);
                expeditionOverlay.SetExpedition(expedition);
                expeditions.Add(expeditionOverlay);
            });
        }
    }
}