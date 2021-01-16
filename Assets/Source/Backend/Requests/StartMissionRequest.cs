namespace Backend.Requests
{
    public class StartMissionRequest
    {
        public string type;
        public int battleTimes;
        public long mapId;
        public int posX;
        public int posY;
        public long vehicleId;
        public long? hero1Id;
        public long? hero2Id;
        public long? hero3Id;
        public long? hero4Id;
    }
}