using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.Buildings
{
    public class Portal : MonoBehaviour
    {
        public Portal targetPortal;

        void Start()
        {
        }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            other.gameObject.transform.position = targetPortal.transform.position + targetPortal.transform.up * 3;
            // other.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    }

}
