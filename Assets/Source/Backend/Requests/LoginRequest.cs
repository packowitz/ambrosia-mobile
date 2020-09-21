using System;

namespace Backend.Requests
{
    [Serializable]
    public struct LoginRequest
    {
        public string email;
        public string password;
    }
}