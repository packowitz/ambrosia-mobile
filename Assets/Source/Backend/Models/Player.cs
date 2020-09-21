using System;

namespace Backend.Models
{
    [Serializable]
    public class Player
    {
        public long id;
        public string name;
        public bool admin;
        public bool betaTester;
        public bool serviceAccount;
        public string color;
    }
}