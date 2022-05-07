using System.Collections.Generic;
using UnityEngine;
using Pandaria.Gatherables;

namespace Pandaria.Islands
{


    public struct IslandResource
    {
        public GatherableResourceController gatherableResourceController;
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
            foreach (var settings in gatherbaleResourceSettingsList)
            {
                islandResourceStates[settings] = new IslandResourceState(){
                    maxCount = 0,
                    islandResources = new List<IslandResource>()
                };
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
            
            foreach (GatherableResourceSettings settings in gatherbaleResourceSettingsList)
            {
                int count = UnityEngine.Random.Range(settings.min, settings.max + 1);
                islandResourceStates[settings].maxCount = 1;

                bool result = SpawnGatherableResourcesOfType(settings);
                if (!result)
                {
                    Debug.Log(string.Format("Not enough slots for resource spawning for island with name {0}", gameObject.name));
                    return;
                }

            }
        }

        private bool SpawnGatherableResourcesOfType(GatherableResourceSettings settings)
        {
                int count = islandResourceStates[settings].maxCount;
                count -= islandResourceStates[settings].islandResources.Count;

                for (int i = 0; i <= count - 1; i++)
                {
                    bool hasNext = freeSpawnPoints.Count > 0;
                    if (!hasNext)
                    {
                        return false;
                    }
                    SpawnGatherableResource(freeSpawnPoints[0], settings);
                    islandResourceStates[settings].lastSpawnTime = Time.time;
                }
                return true;
        }

        private void SpawnGatherableResource(Transform slot, GatherableResourceSettings settings)
        {
            GameObject resource = Instantiate(
                settings.gatherableResource.model,
                slot.position,
                Quaternion.identity,
                slot
            );
            GatherableResourceController gatherableResourceController = resource.AddComponent<GatherableResourceController>();
            gatherableResourceController.Initialize(settings);
            islandResourceStates[settings].islandResources.Add(
                new IslandResource(){
                    slot = slot,
                    gatherableResourceController = gatherableResourceController
                }
            );
            freeSpawnPoints.Remove(slot);
            occupiedSpawnPoints.Add(slot);
        }

        public void OnGatherbaleResourceCollected(object sender, GatherableResourceController gatherableResourceController)
        {
            // For resources that where manually placed in editor
            if (!islandResourceStates.ContainsKey(gatherableResourceController.gatherableResourceSettings))
            {
                return;
            }

            Transform slot = gatherableResourceController.transform.parent;
            freeSpawnPoints.Add(slot);
            occupiedSpawnPoints.Remove(slot);

            var settings = gatherableResourceController.gatherableResourceSettings;
            IslandResource islandResource = islandResourceStates[settings].islandResources.Find(x => x.slot == slot);
            islandResourceStates[settings].islandResources.Remove(islandResource);
        }

        private void PeriodicResourceCheck()
        {
            foreach (var entry in islandResourceStates)
            {
                if (
                    entry.Value.islandResources.Count < entry.Value.maxCount &&
                    Time.time - entry.Key.frequency > entry.Value.lastSpawnTime
                )
                {
                    SpawnGatherableResourcesOfType(entry.Key);
                }
            }
        }
    }

}
