using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ForgeService
    {
        public AutoBreakdownConfiguration AutoBreakdownConfiguration { get; private set; }

        public ForgeService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.autoBreakdownConfiguration != null)
                {
                    AutoBreakdownConfiguration = signal.Data.autoBreakdownConfiguration;
                }
            });
        }
    }
}