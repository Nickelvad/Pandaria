using UnityEngine;
using Pandaria.Characters.Inventory;
using Pandaria.Gatherables;
using Pandaria.Items;

namespace Pandaria.Characters.Actions
{
    public class CharacterGatheringController : MonoBehaviour
    {
        private bool gatheringInProgress = false;
        private float progress = 0f;
        public float gatherSpeed = 3f;
        public CharacterInventoryController characterInventoryController;
        private GatherableResourceController trackedGatherableResource;

        void Start()
        {
            EventBus.Instance.GameObjectSpotted += ProcessSpottedGameObject;
            EventBus.Instance.CharacterMoved += ProcessCharacterMoved;
        }

        void ProcessSpottedGameObject(object sender, GameObject spottedGameObject)
        {
            if (spottedGameObject == null)
            {
                if (trackedGatherableResource != null) 
                {
                    EventBus.Instance.CallGatherableResourceUntracked(this, trackedGatherableResource);
                    trackedGatherableResource = null;
                }
                return;
            }

            GatherableResourceController gatherableResource = spottedGameObject.GetComponent<GatherableResourceController>();
            if (gatherableResource != null)
            {
                trackedGatherableResource = gatherableResource;
                EventBus.Instance.CallGatherableResourceTracked(this, trackedGatherableResource);
            }
        }

        void ProcessCharacterMoved(object sender, Vector3 position)
        {
            if (gatheringInProgress)
            {
                StopGathering(true);
                EventBus.Instance.CallGatherableResourceProgress(this, 0);
            }
        }
        public void DoAction()
        {
            gatheringInProgress = true;
        }

        void Update()
        {
            if (gatheringInProgress)
            {
                progress += Time.deltaTime;
                if (progress >= gatherSpeed)
                {
                    GetResource();
                    StopGathering(false);
                }
                EventBus.Instance.CallGatherableResourceProgress(this, Mathf.RoundToInt(progress * 100 / gatherSpeed));
            }

        }

        void StopGathering(bool cancelled)
        {
            if (!cancelled)
            {
                EventBus.Instance.CallGatherableResourceCollected(this, trackedGatherableResource);
                Destroy(trackedGatherableResource.gameObject);
            }
            EventBus.Instance.CallGatherableResourceUntracked(this, trackedGatherableResource);

            trackedGatherableResource = null;
            progress = 0;
            gatheringInProgress = false;
        }

        void GetResource()
        {
            Item resultItem = trackedGatherableResource.gatherableResourceSettings.gatherableResource.resultItem;
            if (resultItem is PickableItem pickableItem)
            {
                Instantiate(
                    pickableItem.spawnPrefab,
                    trackedGatherableResource.transform.position + Vector3.up,
                    Quaternion.identity
                );
            }

            if (resultItem is InventoryItem inventoryItem)
            {
                characterInventoryController.AddItem(inventoryItem, 1);
            }
        }
    }

}
