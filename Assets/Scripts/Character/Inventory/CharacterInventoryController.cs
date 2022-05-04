using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria;
using Pandaria.Items;

namespace Pandaria.Characters.Inventory
{
    public class CharacterInventoryController : MonoBehaviour
    {
        public int backpackSlotsCount = 21;
        public int quickSlotsCount = 4;
        private List<InventorySlot> backpackSlots;
        private List<InventorySlot> quickSlots;

        void Awake()
        {
            backpackSlots = new List<InventorySlot>(backpackSlotsCount);
            quickSlots = new List<InventorySlot>(quickSlotsCount);
            for (int i = 0; i <= backpackSlotsCount - 1; i++)
            {
                backpackSlots.Add(new InventorySlot());
            }

            for (int i = 0; i <= quickSlotsCount - 1; i++)
            {
                quickSlots.Add(new InventorySlot());
            }
        }

        public bool AddItem(InventoryItem inventoryItem, int amount)
        {
            InventorySlot slot = FindFreeInventorySlotForItem(inventoryItem);
            if (slot == null)
            {
                Debug.Log("Was not able to find slot for new item");
                return false;
            }

            slot.amount += amount;
            slot.inventoryItem = inventoryItem;
            return true;
        }

        public void RemoveItem(InventoryItem inventoryItem, int amount)
        {
            int amountLeft = amount;
            InventorySlot slot = backpackSlots.Find(slot => slot.inventoryItem == inventoryItem);
            if (slot.amount < amountLeft)
            {
                amountLeft -= slot.amount;
                slot.inventoryItem = null;
                RemoveItem(inventoryItem, amountLeft);
            }

            if (slot.amount == amountLeft)
            {
                amountLeft = 0;
                slot.amount = 0;
                slot.inventoryItem = null;
            }

            if (slot.amount > amountLeft)
            {
                slot.amount -= amountLeft;
                amountLeft = 0;
            }
        }

        private InventorySlot FindFreeInventorySlotForItem(InventoryItem inventoryItem)
        {
            InventorySlot slot = backpackSlots.Find(slot => slot.inventoryItem == inventoryItem);
            if (slot != null)
            {
                return slot;
            }

            slot = backpackSlots.Find(slot => slot.inventoryItem == null);
            return slot;
        }

        
    }

}
