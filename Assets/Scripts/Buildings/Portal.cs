using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.Buildings
{
    public class Portal : MonoBehaviour
    {
        public Transform targetPosition;
        public LayerMask whatIsCharacter;

        void OnTriggerEnter(Collider other)
        {

            if (other.gameObject != null && (((1 << other.gameObject.layer) & whatIsCharacter) != 0))
            {
                other.gameObject.transform.position = targetPosition.position;
                Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
            }
        }

    }
}
