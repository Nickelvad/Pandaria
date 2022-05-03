using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Buildings;

namespace Pandaria.Characters
{
    public class SavePortal : MonoBehaviour
    {
        private Rigidbody rigidbody_;
        private bool portalSpawned = false;
        public Portal portal;
        void Start()
        {
            rigidbody_ = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (transform.position.y < -10 && !portalSpawned)
            {
                Vector3 spawnPoint = transform.position - new Vector3(0, 3, 0);
                rigidbody_.velocity = Vector3.down * 3;
                portal.transform.position = spawnPoint;
                portal.gameObject.SetActive(true);
                portalSpawned = true;
            }
        }
    }

}
