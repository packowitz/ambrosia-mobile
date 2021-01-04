using Backend.Models;

namespace Backend.Signal
{
    public class CurrentMapSignal
    {
        public PlayerMap CurrentMap { get; }

        public CurrentMapSignal(PlayerMap newMap)
        {
            CurrentMap = newMap;
        }
    }
}