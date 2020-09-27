using Backend.Responses;

namespace Backend.Signal
{
    public class PlayerActionSignal
    {
        public PlayerActionSignal(PlayerActionResponse data)
        {
            Data = data;
        }

        public PlayerActionResponse Data { get; }
    }
}