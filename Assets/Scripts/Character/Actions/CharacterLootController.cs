using System.Collections.Generic;
using UnityEngine;
using Pandaria.Gatherables;
using Pandaria.Characters.Inventory;
using Pandaria.Items;

namespace Pandaria.Characters.Actions
{
    public class CharacterLootController : MonoBehaviour
    {

        public LootContainer trackedLootContainer;
        public CharacterInventoryController characterInventoryController;

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

        public void TakeAll()
        {
            if (trackedLootContainer == null)
            {
                return;
            }

            List<LootContent> slotsMovedToInventory = new List<LootContent>();
            foreach (LootContent slot in trackedLootContainer.lootContent)
            {
                if (characterInventoryController.AddItem(slot.item, slot.number))
                {
                    slotsMovedToInventory.Add(slot);
                }
            }

            foreach (LootContent slotToDelete in slotsMovedToInventory)
            {
                trackedLootContainer.lootContent.Remove(slotToDelete);
            }
            

        }
    }
}

