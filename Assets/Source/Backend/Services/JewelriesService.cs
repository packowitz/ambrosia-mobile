using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class JewelriesService
    {
        private readonly Dictionary<JewelType, Jewelry> jewelries = new Dictionary<JewelType, Jewelry>();

        public JewelriesService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.jewelries != null)
                {
                    foreach (var jewelry in signal.Data.jewelries)
                    {
                        jewelries[jewelry.type] = jewelry;
                    }
                }
            });
        }

        public Jewelry Jewelry(JewelType type) {
            return jewelries[type];
        }

        public List<Jewelry> Jewelries(GearJewelSlot slot)
        {
            return jewelries.Values.Where(j => j.slot == slot).ToList();
        }

        public Jewelry SpecialJewelry(GearSet set)
        {
            Enum.TryParse($"{set}", out JewelType type);
            var jewelry = jewelries[type];
            return jewelry ?? new Jewelry(type);
        }
    }
}