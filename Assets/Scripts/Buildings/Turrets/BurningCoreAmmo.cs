using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Buildings.Turrets
{
    public class BurningCoreAmmo : BaseTurretAmmo
    {
        public GameObject explosionPrefab;
        public LayerMask whatIsGround;
        override protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == null) { return; }
            if (((1 << other.gameObject.layer) & layersToIgnore) != 0) { return; }
            if (((1 << other.gameObject.layer) & whatIsGround) != 0)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 5f);

            }
            DestroyAmmo();
        }
    }

}
