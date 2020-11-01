using Backend.Models;

namespace Backend.Signal
{
    public class ExpeditionSignal
    {
        public ExpeditionSignal()
        {
        }

        public ExpeditionSignal(PlayerExpedition data)
        {
            Data = data;
        }

        public PlayerExpedition Data { get; }
    }
}