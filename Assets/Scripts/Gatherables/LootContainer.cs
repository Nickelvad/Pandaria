using System;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Items;


namespace Pandaria.Gatherables
{

    [Serializable]
    public struct LootContent
    {
        public InventoryItem item;
        public int number;
    }
    public class LootContainer : MonoBehaviour
    {
        public List<LootContent> lootContent;
    }

}
