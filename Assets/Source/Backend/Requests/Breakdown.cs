using System;
using System.Collections.Generic;

namespace Backend.Requests
{
    [Serializable]
    public struct Breakdown
    {
        public List<long> gearIds;
        public bool silent;
    }
}