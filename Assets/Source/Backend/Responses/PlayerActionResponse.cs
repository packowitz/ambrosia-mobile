using Backend.Models;

namespace Backend.Responses
{
    public struct PlayerActionResponse
    {
        public Player player;
        public Progress progress;
        public Achievements achievements;
    }
}