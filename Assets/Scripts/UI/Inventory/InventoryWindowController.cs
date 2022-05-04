using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        void Awake()
        {
            closeButton.onClick.AddListener(closeDialog);
        }

        void OnDisable()
        {
            CleanSlots(backpackSlotsContainer);
            CleanSlots(quickSlotsContainer);
        }

        void OnEnable()
        {
            FillSlots(characterInventoryController.backpackSlots, backpackSlotPrefab, backpackSlotsContainer);
            FillSlots(characterInventoryController.quickSlots, quickSlotPrefab, quickSlotsContainer);
        }

        void closeDialog()
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

            }
        }
    }
}

