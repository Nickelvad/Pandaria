using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Characters.Inventory;

namespace Pandaria.UI.Inventory
{
    public class InventoryWindowController : MonoBehaviour
    {
        public Button closeButton;
        public CharacterInventoryController characterInventoryController;
        public GameObject backpackSlotPrefab;
        public GameObject quickSlotPrefab;
        public GameObject backpackSlotsContainer;
        public GameObject quickSlotsContainer;
        public Button equipButton;
        private InventorySlotController selectedInventorySlotController;

        void Awake()
        {
            closeButton.onClick.AddListener(OnCloseClick);
            equipButton.onClick.AddListener(OnEquipClick);
        }

        void OnDisable()
        {
            CleanSlots(backpackSlotsContainer);
            CleanSlots(quickSlotsContainer);
            equipButton.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            FillSlots(characterInventoryController.backpackSlots, backpackSlotPrefab, backpackSlotsContainer);
            FillSlots(characterInventoryController.quickSlots, quickSlotPrefab, quickSlotsContainer);
        }

        void OnCloseClick()
        {
            gameObject.SetActive(false);
        }

        private void CleanSlots(GameObject gameObjectForCleanup)
        {
            while (gameObjectForCleanup.transform.childCount > 0)
            {
                DestroyImmediate(gameObjectForCleanup.transform.GetChild(0).gameObject);
            }
        }

        private void Refresh()
        {
            CleanSlots(backpackSlotsContainer);
            CleanSlots(quickSlotsContainer);
            FillSlots(characterInventoryController.backpackSlots, backpackSlotPrefab, backpackSlotsContainer);
            FillSlots(characterInventoryController.quickSlots, quickSlotPrefab, quickSlotsContainer);
            selectedInventorySlotController = null;
        }

        private void FillSlots(List<InventorySlot> slots, GameObject slotPrefab, GameObject slotsContainer)
        {
            foreach (InventorySlot inventorySlot in slots)
            {
                GameObject slotItem = Instantiate(
                    slotPrefab,
                    slotsContainer.transform.position,
                    Quaternion.identity,
                    slotsContainer.transform
                );
                
                InventorySlotController inventorySlotController = slotItem.GetComponent<InventorySlotController>();
                inventorySlotController.Initialize(inventorySlot);
                inventorySlotController.selectButton.onClick.AddListener(() => OnSelectClick(inventorySlotController));
            }
        }

        public void OnSelectClick(InventorySlotController inventorySlotController)
        {
            if (selectedInventorySlotController != null)
            {
                selectedInventorySlotController.SetSelected(false);
            }
            

            selectedInventorySlotController = inventorySlotController;
            selectedInventorySlotController.SetSelected(true);

            if (selectedInventorySlotController.inventorySlot.inventoryItem is EquipableItem equipableItem)
            {
                equipButton.gameObject.SetActive(true);
            }
        }

        public void OnEquipClick()
        {
            if (selectedInventorySlotController.inventorySlot.inventoryItem is EquipableItem equipableItem)
            {
                characterInventoryController.EquipItem(equipableItem);
                Refresh();
            }
            
        }
    }
}

