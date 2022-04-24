using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria;
using Pandaria.Items;

namespace Pandaria.Character.Actions
{
    public class CharacterGatheringController : MonoBehaviour
    {
        private bool gatheringInProgress = false;
        private float progress = 0f;
        public float gatherSpeed = 3f;
        private GatherableResource trackedGatherableResource;
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

            GatherableResource gatherableResource = spottedGameObject.GetComponent<GatherableResource>();
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
                Debug.Log("Stopping");
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
                    SpawnResource();
                    StopGathering(false);
                }
                EventBus.Instance.CallGatherableResourceProgress(this, Mathf.RoundToInt(progress * 100 / gatherSpeed));
            }

        }

        void StopGathering(bool cancelled)
        {
            if (!cancelled)
            {
                trackedGatherableResource.gameObject.SetActive(false);
            }
            EventBus.Instance.CallGatherableResourceUntracked(this, trackedGatherableResource);
            trackedGatherableResource = null;
            progress = 0;
            gatheringInProgress = false;
        }

        void SpawnResource()
        {
            Instantiate(
                trackedGatherableResource.resource,
                trackedGatherableResource.transform.position + Vector3.up,
                Quaternion.identity,
                trackedGatherableResource.transform.parent
            );




        }
    }

}
