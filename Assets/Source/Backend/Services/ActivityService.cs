using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ActivityService
    {
        public List<OddJob> OddJobs { get; private set; }
        public DailyActivity DailyActivity { get; private set; }

        public ActivityService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }
        
        private void Consume(PlayerActionResponse data)
        {
            if (data.oddJobs != null)
            {
                if (OddJobs == null)
                {
                    OddJobs = data.oddJobs;
                }
                else
                {
                    foreach (var oddJob in data.oddJobs)
                    {
                        Update(oddJob);
                    }
                }
            }

            if (data.oddJobDone != null)
            {
                var idx = OddJobs.FindIndex(o => o.id == data.oddJobDone);
                if (idx >= 0)
                {
                    OddJobs.RemoveAt(idx);
                }
            }

            if (data.dailyActivity != null)
            {
                DailyActivity = data.dailyActivity;
            }
        }
        
        private void Update(OddJob oddJob)
        {
            var idx = OddJobs.FindIndex(o => o.id == oddJob.id);
            if (idx >= 0)
            {
                OddJobs[idx] = oddJob;
            }
            else
            {
                OddJobs.Add(oddJob);
            }
        }
    }
}