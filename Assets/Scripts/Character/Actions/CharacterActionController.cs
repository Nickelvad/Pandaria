using System.Collections.Generic;
using UnityEngine;
using Pandaria.Items;

namespace Pandaria.Character.Actions
{
    public class CharacterActionController : MonoBehaviour
    {
        public float maxRange = 5;
        private RaycastHit hit;
        public Transform slot;

        public PickedupItem pickedupItem;

        private Item trackedPickableItem;

        void FixedUpdate()
        {
            Item item = GetItem();

            if (item == trackedPickableItem)
            {
                return;
            }

            if (pickedupItem != null)
            {
                if (trackedPickableItem != null)
                {
                    UntrackPickableItem();
                }
                return;
            }

            if (item == null && trackedPickableItem != null)
            {
                UntrackPickableItem();
            }

            if (item != null && item.isPickable)
            {
                TrackPickableItem(item);
            }

        }

        private Item GetItem()
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxRange))
            {
                Item item = hit.transform.GetComponent<Item>();
                return item;
            }

            return null;
        }

        private void TrackPickableItem(Item item)
        {
            trackedPickableItem = item;
            EventBus.Instance.CallItemTracked(this, trackedPickableItem);
        }

        private void UntrackPickableItem()
        {
            EventBus.Instance.CallItemUntracked(this, trackedPickableItem);
            trackedPickableItem = null;
            
        }

        public void PickupItem()
        {
            if (trackedPickableItem != null)   
            {
                Debug.Log(hit.transform.name.ToString());
                Transform item = trackedPickableItem.transform;
                Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

                pickedupItem = new PickedupItem()
                {
                    item = item,
                    parent = item.parent,
                    rigidbody = itemRigidbody,
                    collider = item.GetComponent<Collider>(),
                    velocity = itemRigidbody.velocity,
                };

                pickedupItem.item.SetParent(slot);
                pickedupItem.rigidbody.velocity = Vector3.zero;
                pickedupItem.rigidbody.isKinematic = true;
                pickedupItem.rigidbody.useGravity = false;
                pickedupItem.item.localPosition = Vector3.zero;
                pickedupItem.item.localEulerAngles = Vector3.zero;
                pickedupItem.collider.enabled = false;
            }

            EventBus.Instance.CallItemPickedup(this, trackedPickableItem);
        }

        public void DropItem()
        {
            if (slot == null) { return; }
            pickedupItem.item.SetParent(pickedupItem.parent);
            pickedupItem.rigidbody.velocity = pickedupItem.velocity;
            pickedupItem.rigidbody.isKinematic = false;
            pickedupItem.rigidbody.useGravity = true;
            pickedupItem.collider.enabled = true;

            pickedupItem.rigidbody.AddForce(transform.forward * 100, ForceMode.Impulse);
            Debug.Log("Calling item dropped");
            EventBus.Instance.CallItemDropped(this, trackedPickableItem);
            pickedupItem = null;
        }
    }

}
