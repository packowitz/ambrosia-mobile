using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class LootBox
    {
        public long id;
        public string name;
        public LootBoxType type;
        public List<LootItem> items;
    }
}