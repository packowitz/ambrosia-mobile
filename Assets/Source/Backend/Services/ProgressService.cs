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
                    var newProgress = signal.Data.progress;
                    var expLvlChanged = Progress != null && newProgress.expeditionLevel != Progress.expeditionLevel;
                    Progress = newProgress;
                    if (expLvlChanged)
                    {
                        signalBus.Fire<ExpeditionLevelSignal>();
                    }
                }
            });
        }
    }
}