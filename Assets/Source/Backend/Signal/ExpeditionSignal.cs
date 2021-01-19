using Backend.Models;
using JetBrains.Annotations;

namespace Backend.Signal
{
    public class ExpeditionSignal
    {
        public ExpeditionSignal() {}
        
        public ExpeditionSignal(PlayerExpedition data)
        {
            Data = data;
        }

        [CanBeNull] public PlayerExpedition Data { get; }
    }
}