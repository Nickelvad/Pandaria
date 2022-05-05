using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pandaria.Characters.Inventory;

namespace Pandaria.UI.Inventory
{
    public class InventorySlotController : MonoBehaviour
    {
        private InventorySlot inventorySlot;
        public Image inventorySlotImage;
        public Text text;
        public void Initialize(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            if (inventorySlot.inventoryItem != null)
            {
                inventorySlotImage.enabled = true;
                inventorySlotImage.sprite = inventorySlot.inventoryItem.inventoryImage;
                text.text = inventorySlot.amount.ToString();
                text.enabled = true;
            }
            else
            {
                inventorySlotImage.enabled = false;
                text.enabled = false;
            }
            
        }
    }
}

