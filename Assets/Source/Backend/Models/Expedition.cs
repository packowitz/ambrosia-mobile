using System;

namespace Backend.Models
{
    [Serializable]
    public class Expedition
    {
        public long id;
        public long secondsAvailable;
        public ExpeditionBase expeditionBase;
    }
}