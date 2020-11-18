using System.Collections.Generic;
using Backend.Models;

namespace Backend.Signal
{
    public class InboxSignal
    {
        public List<InboxMessage> NewMessages { get; }
        public long? MessageDeleted { get; }

        public InboxSignal(List<InboxMessage> newMessages, long? messageDeleted)
        {
            NewMessages = newMessages;
            MessageDeleted = messageDeleted;
        }
    }
}