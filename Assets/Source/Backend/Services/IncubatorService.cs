using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class IncubatorService
    {
        public List<Incubator> Incubators { get; private set; }

        public IncubatorService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.incubators != null)
            {
                if (Incubators == null)
                {
                    Incubators = data.incubators;
                }
                else
                {
                    foreach (var incubator in data.incubators)
                    {
                        Update(incubator);
                    }
                }
            }

            if (data.incubatorDone != null)
            {
                var idx = Incubators.FindIndex(i => i.id == data.incubatorDone);
                if (idx >= 0)
                {
                    Incubators.RemoveAt(idx);
                }
            }
        }

        private void Update(Incubator incubator)
        {
            var idx = Incubators.FindIndex(i => i.id == incubator.id);
            if (idx >= 0)
            {
                Incubators[idx] = incubator;
            }
            else
            {
                Incubators.Add(incubator);
            }
        }
    }
}