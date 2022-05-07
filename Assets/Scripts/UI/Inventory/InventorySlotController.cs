using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pandaria.Characters.Inventory;

namespace Pandaria.UI.Inventory
{
    public class InventorySlotController : MonoBehaviour
    {
        public InventorySlot inventorySlot;
        public Color defaultColor = new Color(03, 03, 03);
        public Color selectedColor = new Color(82, 71, 53);
        public Image inventorySlotBackgroundImage;
        public Image inventorySlotImage;
        public Text text;
        public Button selectButton;
        public void Initialize(InventorySlot inventorySlot)
        {
            selectButton = GetComponent<Button>();
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

        public void SetSelected(bool isSelected)
        {
            inventorySlotBackgroundImage.color = isSelected ? selectedColor : defaultColor;
        }
    }
}

