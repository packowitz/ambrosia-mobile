using Backend.Requests;

namespace Backend
{
    public class PlayerService
    {
        public bool IsLoggedIn => serverAPI.IsLoggedIn;
        private readonly ServerAPI serverAPI;
        
        public PlayerService(ServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }
        
        public void Login(string email, string password)
        {
            var body = new LoginRequest
            {
                email = email,
                password = password
            };
            serverAPI.DoPost("/auth/login", body);
        }
    }
}