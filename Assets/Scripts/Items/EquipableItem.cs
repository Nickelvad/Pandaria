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
        public Vector3 modelPosition;
        public Vector3 modelRotation;
    }

}
