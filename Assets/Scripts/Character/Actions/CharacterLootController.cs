using UnityEngine;
using Pandaria.Gatherables;

namespace Pandaria.Characters.Actions
{
    public class CharacterLootController : MonoBehaviour
    {
        public LootContainer trackedLootContainer;
        void Awake()
        {
            EventBus.Instance.GameObjectSpotted += ProcessSpottedGameObject;
        }

        void ProcessSpottedGameObject(object sender, GameObject spottedGameObject)
        {
            LootContainer lootContainer = GetLootContainer(spottedGameObject);
            if (lootContainer == trackedLootContainer)
            {
                return;
            }

            if (lootContainer == null && trackedLootContainer != null)
            {
                UntrackLootContainer();
            }

            if (lootContainer != null && trackedLootContainer == null)
            {
                TrackLootContainer(lootContainer);
            }
        }

        private LootContainer GetLootContainer(GameObject spottedGameObject)
        {   if (spottedGameObject == null)
            {
                return null;
            }
            LootContainer lootContainer = spottedGameObject.transform.GetComponent<LootContainer>();
            return lootContainer;
        }

        private void TrackLootContainer(LootContainer lootContainer)
        {
            trackedLootContainer = lootContainer;
            EventBus.Instance.CallLootContainerTracked(this, trackedLootContainer);
        }

        private void UntrackLootContainer()
        {
            EventBus.Instance.CallLootContainerUntracked(this, trackedLootContainer);
            trackedLootContainer = null;
        }

        // public void DoAction()
        // {
        //     if (trackedLootContainer == null)
        //     {
        //         return;
        //     }

        //     EventBus.Instance.CallLootContainerOpen(this, trackedLootContainer);
        // }
    }
}

