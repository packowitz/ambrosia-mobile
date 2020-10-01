using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ProgressService
    {
        public Progress Progress { get; private set; }
        
        public ProgressService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.progress != null)
                {
                    Progress = signal.Data.progress;
                }
            });
        }
    }
}