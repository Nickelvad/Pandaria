using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria;
using Pandaria.Items;

namespace Pandaria.Characters.Inventory
{

    public class SlotStatus
    {
        public GameObject parent;
        public GameObject equipmentItem;
    }
    public class WeaponSlotStatus : SlotStatus
    {
        public Weapon weapon;
    }

    public class ArmorSlotStatus : SlotStatus
    {
        public Armor armor;
    }

    public class CharacterInventoryController : MonoBehaviour
    {
        public int backpackSlotsCount = 21;
        public int quickSlotsCount = 4;
        [SerializeField] public List<InventorySlot> backpackSlots {get; private set;}
        [SerializeField] public List<InventorySlot> quickSlots {get; private set;}

        public GameObject primaryWeaponParent;
        public GameObject secondaryWeaponParent;
        public Dictionary<WeaponSlot, WeaponSlotStatus> equipedWeapons;
        public Dictionary<ArmorSlot, ArmorSlotStatus> equipedArmor;
        void Awake()
        {
            equipedWeapons = new Dictionary<WeaponSlot, WeaponSlotStatus>()
            {
                {WeaponSlot.Primary, new WeaponSlotStatus(){parent = primaryWeaponParent}},
                {WeaponSlot.Secondary, new WeaponSlotStatus(){parent = secondaryWeaponParent}}
            };
            equipedArmor = new Dictionary<ArmorSlot, ArmorSlotStatus>()
            {
                {ArmorSlot.Helm, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Shoulders, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Chest, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Arms, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Legs, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Boots, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Necklace, new ArmorSlotStatus(){parent = null}},
                {ArmorSlot.Ring, new ArmorSlotStatus(){parent = null}},
            };
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

        public bool EquipSlot(InventorySlot slot)
        {
            if (slot.inventoryItem is EquipableItem equipableItem)
            {
                slot.inventoryItem = null;
                bool switched = EquipItem(equipableItem);
                if (switched)
                {
                    EventBus.Instance.CallEquipmentChanged(this, equipableItem);
                }
                return switched;
            }
            return false;
        }
        public bool EquipItem(EquipableItem item)
        {
            if (item is Weapon weapon)
            {
                var weaponSlotStatus = equipedWeapons[weapon.weaponSlot];
                if (weaponSlotStatus.weapon != null)
                {
                    bool movedToBackpack = AddItem(weaponSlotStatus.weapon, 1);
                    if (!movedToBackpack)
                    {
                        return false;
                    }
                }
                
                ChangeEquipment(weaponSlotStatus.equipmentItem, item, weaponSlotStatus.parent);
                weaponSlotStatus.weapon = weapon;
                return true;
            }

            return false;
        }

        private void ChangeEquipment(GameObject oldItem, EquipableItem newItem, GameObject parent)
        {
            DestroyImmediate(oldItem);
            var createdItem = Instantiate(newItem.model, parent.transform.position, newItem.model.transform.rotation, parent.transform);
            createdItem.transform.localPosition = newItem.modelPosition;
            createdItem.transform.localEulerAngles = newItem.modelRotation;
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
