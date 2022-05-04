using System;
using Pandaria.Items;

namespace Pandaria.Characters.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public InventoryItem inventoryItem;
        public int amount = 0;
    }

}
