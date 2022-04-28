using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Resources
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Gatherable Resource", menuName = "Scriptables/Gatherable Resource", order=1)]
    public class GatherableResource : ScriptableObject
    {
        public string resourceName;
        public GameObject model;
        public GameObject modelAfterGathering;
        public int minPerInstance;
        public int maxPerInstance;
    }
}
