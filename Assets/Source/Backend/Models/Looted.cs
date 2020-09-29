using System;
using System.Collections.Generic;

namespace Backend.Models
{
    [Serializable]
    public class Looted
    {
        public LootedType type;
        public List<LootedItem> items;

        // transient
        public bool autobreakdownChecked;
    }
}