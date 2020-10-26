using Backend.Models;

namespace Backend.Signal
{
    public class ResourcesSignal
    {
        public ResourcesSignal(Resources data)
        {
            Data = data;
        }

        public Resources Data { get; }
    }
}