using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class UpgradeService
    {
        public List<Upgrade> Upgrades { get; private set; }

        public UpgradeService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.upgrades != null)
            {
                if (Upgrades == null)
                {
                    Upgrades = data.upgrades;
                    SortUpgrades();
                }
                else
                {
                    foreach (var upgrade in data.upgrades)
                    {
                        Update(upgrade);
                    }
                }
            }

            if (data.upgradeRemoved != null)
            {
                var idx = Upgrades.FindIndex(u => u.id == data.upgradeRemoved);
                if (idx >= 0)
                {
                    Upgrades.RemoveAt(idx);
                }
            }
        }

        private void Update(Upgrade upgrade)
        {
            var idx = Upgrades.FindIndex(u => u.id == upgrade.id);
            if (idx >= 0)
            {
                Upgrades[idx] = upgrade;
            }
            else
            {
                Upgrades.Add(upgrade);
                SortUpgrades();
            }
        }

        private void SortUpgrades()
        {
            Upgrades = Upgrades.OrderBy(u => u.position).ToList();
        }
    }
}