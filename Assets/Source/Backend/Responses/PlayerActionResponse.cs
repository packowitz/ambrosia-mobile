using Backend.Models;
using Newtonsoft.Json;

namespace Backend.Responses
{
    public struct PlayerActionResponse
    {
        public Player player;
        public Progress progress;
        public Achievements achievements;
        public string token;
        public Resources resources;
        public Hero hero;
        public Hero[] heroes;
        public long[] heroIdsRemoved;
        public Gear gear;
        public Gear[] gears;
        public long[] gearIdsRemovedFromArmory;
        public Jewelry[] jewelries;
        public Building[] buildings;
        public Vehicle[] vehicles;
        public VehiclePart[] vehicleParts;
        public PlayerMap[] playerMaps;
        public PlayerMap currentMap;
        public Battle ongoingBattle;
        public Looted looted;
        public Mission[] missions;
        public long? missionIdFinished;
    }
}