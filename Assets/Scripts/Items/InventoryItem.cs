using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Items
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptables/Items/Inventory Item", order=1)]
    public class InventoryItem : Item
    {
        public Sprite inventoryImage;
        
    }

}
