using System.Collections.Generic;
using UnityEngine;
using Pandaria.Resources;

namespace Pandaria.Islands
{


    public struct IslandResource
    {
        public GatherableResourceManager gatherableResourceManager;
        public Transform slot;
    }

    public class IslandResourceState
    {
        public int maxCount;
        public float lastSpawnTime;
        public List<IslandResource> islandResources;
    }

    public class Island : MonoBehaviour
    {
        public List<Transform> spawnPoints;
        public List<GatherableResourceSettings> gatherbaleResourceSettingsList;
        private Dictionary<GatherableResourceSettings, IslandResourceState> islandResourceStates;

        private List<Transform> freeSpawnPoints;
        private List<Transform> occupiedSpawnPoints = new List<Transform>();

        void Awake()
        {
            freeSpawnPoints = new List<Transform>(spawnPoints);
            islandResourceStates = new Dictionary<GatherableResourceSettings, IslandResourceState>();
            foreach (var item in gatherbaleResourceSettingsList)
            {
                islandResourceStates[item] = new IslandResourceState(){maxCount = 0, islandResources = new List<IslandResource>()};
            }
            SpawnInitialResources();
            EventBus.Instance.GatherableResourceCollected += OnGatherbaleResourceCollected;
            InvokeRepeating(nameof(PeriodicResourceCheck), 2f, 1f);
        }

        void OnDestroy()
        {
            CancelInvoke(nameof(PeriodicResourceCheck));
        }

        private void SpawnInitialResources()
        {
            
            foreach (GatherableResourceSettings gatherableResourceSettings in gatherbaleResourceSettingsList)
            {
                int count = Random.Range(gatherableResourceSettings.min, gatherableResourceSettings.max + 1);
                islandResourceStates[gatherableResourceSettings].maxCount = 1;

                bool result = SpawnGatherableResourcesOfType(gatherableResourceSettings);
                if (!result)
                {
                    Debug.Log(string.Format("Not enough slots for resource spawning for island with name {0}", gameObject.name));
                    return;
                }

            }
        }

        private bool SpawnGatherableResourcesOfType(GatherableResourceSettings gatherableResourceSettings)
        {
                int count = islandResourceStates[gatherableResourceSettings].maxCount;
                count -= islandResourceStates[gatherableResourceSettings].islandResources.Count;

                for (int i = 0; i <= count - 1; i++)
                {
                    bool hasNext = freeSpawnPoints.Count > 0;
                    if (!hasNext)
                    {
                        return false;
                    }
                    SpawnGatherableResource(freeSpawnPoints[0], gatherableResourceSettings);
                    islandResourceStates[gatherableResourceSettings].lastSpawnTime = Time.time;
                }
                return true;
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
            islandResourceStates[gatherableResourceSettings].islandResources.Add(
                new IslandResource(){slot = slot, gatherableResourceManager = gatherableResourceManager}
            );
            freeSpawnPoints.Remove(slot);
            occupiedSpawnPoints.Add(slot);
        }

        public void OnGatherbaleResourceCollected(object sender, GatherableResourceManager gatherableResourceManager)
        {
            Transform slot = gatherableResourceManager.transform.parent;
            freeSpawnPoints.Add(slot);
            occupiedSpawnPoints.Remove(slot);
            IslandResource islandResource = islandResourceStates[gatherableResourceManager.gatherableResourceSettings].islandResources.Find(x => x.slot == slot);
            islandResourceStates[gatherableResourceManager.gatherableResourceSettings].islandResources.Remove(islandResource);


        }

        private void PeriodicResourceCheck()
        {
            foreach (var item in islandResourceStates)
            {
                if (item.Value.islandResources.Count < item.Value.maxCount && Time.time - item.Key.frequency > item.Value.lastSpawnTime)
                {
                    SpawnGatherableResourcesOfType(item.Key);
                }
            }
        }
    }

}
