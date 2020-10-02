using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class LootedService
    {
        public Looted Looted { get; private set; }
        
        public LootedService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.looted != null)
                {
                    Looted = signal.Data.looted;
                }
            });
        }

        public void Dismiss()
        {
            Looted = null;
        }
    }
}