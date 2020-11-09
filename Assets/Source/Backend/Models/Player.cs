using System;
using Backend.Models.Enums;

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
        public Color color;
    }
}