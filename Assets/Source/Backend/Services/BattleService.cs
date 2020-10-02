using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class BattleService
    {
        public Battle ongoingBattle{ get; private set; }
        
        public BattleService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.ongoingBattle != null)
                {
                    ongoingBattle = signal.Data.ongoingBattle;
                }
            });
        }
    }
}