using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Character.Actions
{
    public class CharacterActionController : MonoBehaviour
    {
        public float maxRange = 5;
        private RaycastHit hit;
        public Transform slot;
        public EventBus eventBus;

        private InitialItemState initialItemState;

        private List<CharacterAction> activeActions;

        void Start()
        {
            activeActions = new List<CharacterAction>();
        }

        void FixedUpdate()
        {

            CharacterAction action = GetAction();
            if (action != null)
            {
                AddActiveAction(action);
            }

        }

        private CharacterAction GetAction()
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxRange))
            {
                CharacterAction action = hit.transform.GetComponent<CharacterAction>();
                return action;
            }

            return null;
        }

        public void DoAction(CharacterAction action)
        {
           if (action != null)
            {
                switch (action.actionType)
                {
                    case CharacterActionType.PickUp:
                        PickUpItem(action);
                        break;
                    case CharacterActionType.Drop:
                        DropItem(action);
                        break;
                }
            }
        }

        private void AddActiveAction(CharacterAction action)
        {
            activeActions.Clear();
            activeActions.Add(action);
            eventBus.CallActionsChanged(this, activeActions);
        }

        public void PickUpItem(CharacterAction action)
        {
            if (action != null)   
            {
                Debug.Log(hit.transform.name.ToString());
                Transform item = action.transform;
                Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

                initialItemState = new InitialItemState()
                {
                    item = item,
                    parent = item.parent,
                    rigidbody = itemRigidbody,
                    collider = item.GetComponent<Collider>(),
                    velocity = itemRigidbody.velocity,
                };

                initialItemState.item.SetParent(slot);
                initialItemState.rigidbody.velocity = Vector3.zero;
                initialItemState.rigidbody.isKinematic = true;
                initialItemState.rigidbody.useGravity = false;
                initialItemState.item.localPosition = Vector3.zero;
                initialItemState.item.localEulerAngles = Vector3.zero;
                initialItemState.collider.enabled = false;
            }
        }

        public void DropItem(CharacterAction action)
        {
            if (slot == null) { return; }
            initialItemState.item.SetParent(initialItemState.parent);
            initialItemState.rigidbody.velocity = initialItemState.velocity;
            initialItemState.rigidbody.isKinematic = true;
            initialItemState.rigidbody.useGravity = true;
            initialItemState.collider.enabled = true;

            initialItemState.rigidbody.AddForce(transform.forward * 100, ForceMode.Impulse);

            initialItemState = null;
        }
    }

}
