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
                    progress = 0;
                    gatheringInProgress = false;
                    SpawnResource();
                    trackedGatherableResource.gameObject.SetActive(false);
                    trackedGatherableResource = null;
                }
                EventBus.Instance.CallGatherableResourceProgress(this, Mathf.RoundToInt(progress * 100 / gatherSpeed));
                Debug.Log(progress);
            }

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
