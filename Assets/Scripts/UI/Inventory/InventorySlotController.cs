using UnityEngine;
using UnityEngine.UI;
using Pandaria.Characters.Inventory;

namespace Pandaria.UI.Inventory
{
    public class InventorySlotController : MonoBehaviour
    {
        private InventorySlot inventorySlot;
        public Image inventorySlotImage;

        public void Initialize(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            if (inventorySlot.inventoryItem != null)
            {
                inventorySlotImage.enabled = true;
                inventorySlotImage.sprite = inventorySlot.inventoryItem.inventoryImage;
            }
            else
            {
                inventorySlotImage.enabled = false;
            }
            
        }
    }
}

