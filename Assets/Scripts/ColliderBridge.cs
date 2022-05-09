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
            colliderListener = listener;
        }

        void OnCollisionEnter(Collision collision)
        {
            colliderListener.ExtraOnCollisionEnter(this.gameObject, collision);
        }
        void OnTriggerEnter(Collider other)
        {
            colliderListener.ExtraOnTriggerEnter(this.gameObject, other);
        }
    }
}

 