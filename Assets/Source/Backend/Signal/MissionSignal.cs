using Backend.Models;

namespace Backend.Signal
{
    public class MissionSignal
    {
        public MissionSignal(Mission data)
        {
            Data = data;
        }

        public Mission Data { get; }
    }
}