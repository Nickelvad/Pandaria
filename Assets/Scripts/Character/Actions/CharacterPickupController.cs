using System.Collections.Generic;
using UnityEngine;
using Pandaria.Items;

namespace Pandaria.Characters.Actions
{
    public class CharacterPickupController : MonoBehaviour
    {
        private RaycastHit hit;
        public Transform slot;
        public PickedupItemContainer pickedupItemContainer;
        private ItemContainer trackedPickableItemContainer;

        void Start()
        {
            EventBus.Instance.GameObjectSpotted += ProcessSpottedGameObject;
        }

        void ProcessSpottedGameObject(object sender, GameObject spottedGameObject)
        {
            ItemContainer itemContainer = GetItem(spottedGameObject);

            if (itemContainer == trackedPickableItemContainer)
            {
                return;
            }

            if (pickedupItemContainer != null)
            {
                if (trackedPickableItemContainer != null)
                {
                    UntrackPickableItemContainer();
                }
                return;
            }

            if (itemContainer == null && trackedPickableItemContainer != null)
            {
                UntrackPickableItemContainer();
            }

            if (itemContainer != null && itemContainer.isPickable)
            {
                TrackPickableItemContainer(itemContainer);
            }

        }

        private ItemContainer GetItem(GameObject spottedGameObject)
        {   if (spottedGameObject == null)
            {
                return null;
            }
            ItemContainer itemContainer = spottedGameObject.transform.GetComponent<ItemContainer>();
            return itemContainer;
        }

        private void TrackPickableItemContainer(ItemContainer itemContainer)
        {
            trackedPickableItemContainer = itemContainer;
            EventBus.Instance.CallItemContainerTracked(this, trackedPickableItemContainer);
        }

        private void UntrackPickableItemContainer()
        {
            EventBus.Instance.CallItemContainerUntracked(this, trackedPickableItemContainer);
            trackedPickableItemContainer = null;
            
        }

        public void PickupItem()
        {
            if (trackedPickableItemContainer != null)   
            {
                Transform itemContainer = trackedPickableItemContainer.transform;
                Rigidbody itemRigidbody = itemContainer.GetComponent<Rigidbody>();

                pickedupItemContainer = new PickedupItemContainer()
                {
                    itemContainer = itemContainer,
                    parent = itemContainer.parent,
                    rigidbody = itemRigidbody,
                    collider = itemContainer.GetComponent<Collider>(),
                    velocity = itemRigidbody.velocity,
                };

                pickedupItemContainer.itemContainer.SetParent(slot);
                pickedupItemContainer.rigidbody.velocity = Vector3.zero;
                pickedupItemContainer.rigidbody.isKinematic = true;
                pickedupItemContainer.rigidbody.useGravity = false;
                pickedupItemContainer.itemContainer.localPosition = Vector3.zero;
                pickedupItemContainer.itemContainer.localEulerAngles = Vector3.zero;
                pickedupItemContainer.collider.enabled = false;
            }

            EventBus.Instance.CallItemContainerPickedup(this, trackedPickableItemContainer);
        }

        public void DropItemContainer()
        {
            if (slot == null) { return; }
            pickedupItemContainer.itemContainer.SetParent(pickedupItemContainer.parent);
            pickedupItemContainer.rigidbody.velocity = pickedupItemContainer.velocity;
            pickedupItemContainer.rigidbody.isKinematic = false;
            pickedupItemContainer.rigidbody.useGravity = true;
            pickedupItemContainer.collider.enabled = true;
            pickedupItemContainer.rigidbody.AddForce(transform.forward * 100, ForceMode.Impulse);
            EventBus.Instance.CallItemContainerDropped(this, trackedPickableItemContainer);
            pickedupItemContainer = null;
        }
    }

}
