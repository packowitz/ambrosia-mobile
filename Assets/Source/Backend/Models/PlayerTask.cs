using System;

namespace Backend.Models
{
    [Serializable]
    public class PlayerTask
    {
        public long id;
        public long playerId;
        public long taskClusterId;
        public int currentTaskNumber;
    }
}