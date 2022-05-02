using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria
{
    public class ColliderBridge : MonoBehaviour
    {
        public IColliderListener colliderListener;

        public void Initialize(IColliderListener listener)
        {
            Debug.Log("Initialized");
            colliderListener = listener;
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            colliderListener.ExtraOnCollisionEnter(this.gameObject, collision);
        }
        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            colliderListener.ExtraOnTriggerEnter(this.gameObject, other);
        }
    }
}

 