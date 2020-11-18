using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class InboxMessage
    {
        public long id;
        public long playerId;
        public InboxMessageType messageType;
        public long? senderId;
        public bool read;
        public string message;
        public List<InboxMessageItem> items;
        public long ageInSeconds;
        public int validInSeconds;
        public DateTime ValidTime { get; private set; }
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            ValidTime = DateTime.Now + TimeSpan.FromSeconds(validInSeconds);
        }
    }
}