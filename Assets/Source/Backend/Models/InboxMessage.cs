using System;

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
        public InboxMessageItem[] items;
        public long ageInSeconds;
        public int validInSeconds;
    }
}