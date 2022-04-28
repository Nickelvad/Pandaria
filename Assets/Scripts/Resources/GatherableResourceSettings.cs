using System;

namespace Pandaria.Resources
{
    [Serializable]
    public class GatherableResourceSettings
    {
        public GatherableResource gatherableResource;
        public int min = 0;
        public int max = 2;
        public float frequency = 60f;
    }
}