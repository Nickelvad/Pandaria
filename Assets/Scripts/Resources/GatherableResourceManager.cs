using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Resources
{
    public class GatherableResourceManager : MonoBehaviour
    {
        public GatherableResourceSettings gatherableResourceSettings;

        public void Initialize(GatherableResourceSettings gatherableResourceSettings)
        {
            this.gatherableResourceSettings = gatherableResourceSettings;
        }
    }
}

