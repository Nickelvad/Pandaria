using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria
{
    public interface IColliderListener
    {
        public void ExtraOnCollisionEnter(Collision collision);
        public void ExtraOnTriggerEnter(Collider other);

    }
}
