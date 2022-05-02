using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria
{
    public interface IColliderListener
    {
        public void ExtraOnCollisionEnter(GameObject notifier, Collision collision);
        public void ExtraOnTriggerEnter(GameObject notifier, Collider other);

    }
}
