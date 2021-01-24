using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ActivityService
    {
        [Inject] private ServerAPI serverAPI;
        private SignalBus signalBus;
        public List<OddJob> OddJobs { get; private set; }
        public DailyActivity DailyActivity { get; private set; }

        public ActivityService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public void ClaimDaily(int day, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/oddjob/daily/{day}", null, onSuccess);
        }

        public void RemoveOddJob(OddJob oddJob, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/oddjob/{oddJob.id}/remove", null, onSuccess);
        }

        public void ClaimOddJob(OddJob oddJob, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/oddjob/{oddJob.id}/claim", null, onSuccess);
        }

        public bool HasClaimableActivity()
        {
            if (OddJobs.Exists(o => o.jobAmountDone >= o.jobAmount))
            {
                return true;
            }

            for (var i = 1; i <= DailyActivity.today; i++)
            {
                if (DailyActivity.Claimable(i))
                {
                    return true;
                }
            }

            return false;
        }
        
        private void Consume(PlayerActionResponse data)
        {
            var needSignal = false;
            if (data.oddJobs != null)
            {
                needSignal = true;
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
                needSignal = true;
                var idx = OddJobs.FindIndex(o => o.id == data.oddJobDone);
                if (idx >= 0)
                {
                    OddJobs.RemoveAt(idx);
                }
            }

            if (data.dailyActivity != null)
            {
                needSignal = true;
                DailyActivity = data.dailyActivity;
            }

            if (needSignal)
            {
                signalBus.Fire<ActivitySignal>();
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