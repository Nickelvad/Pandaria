using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Items
{

    public enum ArmorSlot
    {
        Helm,
        Shoulders,
        Chest,
        Arms,
        Legs,
        Boots,
        Necklace,
        Ring,
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "ArmorItem", menuName = "Scriptables/Items/Armor Item", order=2)]
    public class Armor : EquipableItem
    {
        public ArmorSlot armorSlot;
        public int defence = 0;
    }

}
