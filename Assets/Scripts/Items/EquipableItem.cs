using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Items
{
    public enum EquipableSlotType
    {
        Armor,
        Weapon,
    }
    public class EquipableItem : InventoryItem
    {
        public EquipableSlotType equipableSlotType;
        public GameObject model;
    }

}
