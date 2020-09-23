using System;

namespace Backend.Models
{
    [Serializable]
    public class Looted
    {
        public LootedType type;
        public LootedItem[] items;

        // transient
        public bool autobreakdownChecked;
    }
}