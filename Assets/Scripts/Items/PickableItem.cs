using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Items
{

    [System.Serializable]
    [CreateAssetMenu(fileName = "PickableItem", menuName = "Scriptables/Items/Pickable Item", order=1)]
    public class PickableItem : Item
    {
        public GameObject spawnPrefab;
    }

}
