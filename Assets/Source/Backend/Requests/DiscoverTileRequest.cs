using System;
using System.Collections.Generic;

namespace Backend.Requests
{
    [Serializable]
    public struct DiscoverTileRequest
    {
        public long mapId;
        public int posX;
        public int posY;
    }
}