using Backend.Models;

namespace Backend.Signal
{
    public class MissionSignal
    {
        public MissionSignal(Mission data, bool finished)
        {
            Data = data;
            Finished = finished;
        }

        public Mission Data { get; }
        public bool Finished { get; }
    }
}