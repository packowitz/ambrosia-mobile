using System;

namespace Backend.Requests
{
    [Serializable]
    public struct RegisterRequest
    {
        public string name;
        public string email;
        public string password;
    }
}