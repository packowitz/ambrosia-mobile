using System;

namespace Backend.Models
{
    [Serializable]
    public class LootBox
    {
        public long id;
        public string name;
        public LootBoxType type;
        public LootItem[] items;
    }
}