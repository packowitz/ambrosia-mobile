using Backend.Requests;
using Backend.Responses;
using UnityEngine;

namespace Backend
{
    public class PlayerService
    {
        private ServerAPI serverAPI;
        
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
            serverAPI.DoPost<PlayerActionResponse>("/auth/login", body, resp =>
            {
                Debug.Log(resp.player?.name + " logged in");
            }, null);
        }
    }
}