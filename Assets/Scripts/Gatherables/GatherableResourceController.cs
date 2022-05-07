using UnityEngine;

namespace Pandaria.Gatherables
{
    public class GatherableResourceController : MonoBehaviour
    {
        public GatherableResourceSettings gatherableResourceSettings;

        public void Initialize(GatherableResourceSettings gatherableResourceSettings)
        {
            this.gatherableResourceSettings = gatherableResourceSettings;
        }
    }
}

