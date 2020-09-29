using System;
using System.Collections.Generic;

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
    }
}