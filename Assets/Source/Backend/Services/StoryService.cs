using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class StoryService
    {
        private readonly List<StoryTrigger> knownStories = new List<StoryTrigger>();

        public StoryService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.knownStories != null)
                {
                    knownStories.AddRange(signal.Data.knownStories);
                }
            });
        }

        public bool StoryUnknown(StoryTrigger story)
        {
            return !knownStories.Contains(story);
        }
    }
}