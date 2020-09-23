using Backend.Models;
using Backend.Models.ObjectShape;

namespace Backend
{
    public class LootedItem
    {
        public LootedItemType type;
        public ResourceType resourceType;
        public ProgressStat progressStat;
        public JewelTypeObj jewelType;
        public long value;
    }
}