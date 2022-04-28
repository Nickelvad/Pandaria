using System.Collections.Generic;
using UnityEngine;
using Pandaria.Resources;

namespace Pandaria.Islands
{

    public class Island : MonoBehaviour
    {
        public List<Transform> resourceSpawnPoints;
        public List<GatherableResourceSettings> gatherbaleResourceSettingsList;
        public List<GatherableResourceManager> resources;

        void Awake()
        {
            if (resources == null)
            {
                resources = new List<GatherableResourceManager>();
            }
            foreach (GatherableResourceSettings gatherableResourceSettings in gatherbaleResourceSettingsList)
            {
                Transform slot = resourceSpawnPoints[Random.Range(0, resourceSpawnPoints.Count - 1)];
                SpawnGatherableResource(slot, gatherableResourceSettings);
            }
        }

        private void SpawnGatherableResource(Transform slot, GatherableResourceSettings gatherableResourceSettings)
        {
            GameObject resource = Instantiate(
                gatherableResourceSettings.gatherableResource.model,
                slot.position,
                Quaternion.identity,
                slot
            );
            GatherableResourceManager gatherableResourceManager = resource.AddComponent<GatherableResourceManager>();
            gatherableResourceManager.Initialize(gatherableResourceSettings);
            resources.Add(gatherableResourceManager);
        }
    }

}
