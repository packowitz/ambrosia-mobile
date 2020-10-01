using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class GearService
    {
        private List<Gear> gears;

        public GearService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public List<Gear> AllGears()
        {
            return gears;
        }

        public Gear Gear(long id)
        {
            return gears.Find(h => h.id == id);
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.gears != null)
            {
                if (gears == null)
                {
                    gears = data.gears;
                }
                else
                {
                    foreach (var gear in data.gears)
                    {
                        Update(gear);
                    }
                }
            }

            if (data.gear != null)
            {
                Update(data.gear);
            }

            if (data.gearIdsRemovedFromArmory != null)
            {
                gears = gears.Where(h => !data.heroIdsRemoved.Contains(h.id)).ToList();
            }
        }

        private void Update(Gear gear)
        {
            var idx = gears.FindIndex(g => g.id == gear.id);
            if (idx >= 0)
            {
                gears[idx] = gear;
            }
            else
            {
                gears.Add(gear);
            }
        }
    }
}