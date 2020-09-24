using System;

namespace Backend.Models
{
    [Serializable]
    public class Incubator
    {
        public long id;
        public long playerId;
        public GenomeType type;
        public bool finished;
        public int duration;
        public int secondsUntilDone;
    }
}