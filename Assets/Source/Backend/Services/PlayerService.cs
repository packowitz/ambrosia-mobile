using System;
using Backend.Models;
using Backend.Requests;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class PlayerService
    {
        private readonly ServerAPI serverAPI;

        public Player Player { get; private set; }

        public bool PlayerInitialized => Player?.name != null;
        
        public PlayerService(ServerAPI serverAPI, SignalBus signalBus)
        {
            this.serverAPI = serverAPI;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.player != null)
                {
                    Player = signal.Data.player;
                }
            });
        }
        
        public void Login(string email, string password, Action<PlayerActionResponse> onSuccess)
        {
            var body = new LoginRequest
            {
                email = email,
                password = password
            };
            serverAPI.DoPost("/auth/login", body, onSuccess);
        }
        
        public void Register(string ign, string email, string password, Action<PlayerActionResponse> onSuccess)
        {
            var body = new RegisterRequest
            {
                name = ign,
                email = email,
                password = password
            };
            serverAPI.DoPost("/auth/register", body, onSuccess);
        }

        public void LoadPlayer()
        {
            serverAPI.DoPost("/player");
        }
    }
}