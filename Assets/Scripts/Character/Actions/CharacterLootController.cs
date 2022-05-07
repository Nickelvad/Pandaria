using UnityEngine;
using Pandaria.Gatherables;

namespace Pandaria.Characters.Actions
{
    public class CharacterLootController : MonoBehaviour
    {
        private LootContainer trackedLootContainer;
        void Awake()
        {
            EventBus.Instance.GameObjectSpotted += ProcessSpottedGameObject;
        }

        void ProcessSpottedGameObject(object sender, GameObject spottedGameObject)
        {
            LootContainer lootContainer = GetLootContainer(spottedGameObject);;
            if (lootContainer == trackedLootContainer)
            {
                return;
            }

            if (lootContainer != null)
            {
                if (trackedLootContainer != null)
                {
                    UntrackLootContainer();
                }
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
            // EventBus.Instance.CallItemContainerTracked(this, trackedLootContainer);
        }

        private void UntrackLootContainer()
        {
            // EventBus.Instance.CallItemContainerUntracked(this, trackedLootContainer);
            trackedLootContainer = null;
            
        }
    }
}

