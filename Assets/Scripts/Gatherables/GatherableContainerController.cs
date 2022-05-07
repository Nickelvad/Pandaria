using System;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Items;


namespace Pandaria.Gatherables
{

    [Serializable]
    public struct GatherableContent
    {
        public InventoryItem item;
        public int number;
    }
    public class GatherableContainerController : MonoBehaviour
    {
        public List<GatherableContent> gatherableContent;
    }

}
