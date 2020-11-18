using System;
using System.Runtime.Serialization;

namespace Backend.Models
{
    [Serializable]
    public class Expedition
    {
        public long id;
        public long secondsAvailable;
        public DateTime AvailableTime { get; private set; }
        public ExpeditionBase expeditionBase;
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            AvailableTime = DateTime.Now + TimeSpan.FromSeconds(secondsAvailable);
        }
    }
}