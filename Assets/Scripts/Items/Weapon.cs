using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Items
{

    public enum WeaponSlot
    {
        Primary,
        Secondary,
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptables/Items/Weapon Item", order=3)]
    public class Weapon : EquipableItem
    {
        public WeaponSlot weaponSlot;
        public int minAttack;
        public int maxAttack;
        public int critRate;
    }

}
