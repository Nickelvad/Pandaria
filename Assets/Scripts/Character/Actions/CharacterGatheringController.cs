using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria;
using Pandaria.Items;

namespace Pandaria.Character.Actions
{
    public class CharacterGatheringController : MonoBehaviour
    {
        void Start()
        {
            EventBus.Instance.GameObjectSpotted += ProcessSpottedGameObject;
        }

        void ProcessSpottedGameObject(object sender, GameObject spottedGameObject)
        {
            if (spottedGameObject == null)
            {
                return;
            }

            GatherableResource gatherableResource = spottedGameObject.GetComponent<GatherableResource>();
            if (gatherableResource == null)
            {
                return;
            }
            Debug.Log("Gather");
        }

        void Update()
        {
            
        }
    }

}
