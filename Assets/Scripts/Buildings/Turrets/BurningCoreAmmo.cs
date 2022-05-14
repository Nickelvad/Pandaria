using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Enemies;

namespace Pandaria.Buildings.Turrets
{
    public class BurningCoreAmmo : BaseTurretAmmo
    {
        public GameObject explosionPrefab;
        public LayerMask whatIsGround;
        public LayerMask whatIsEnemy;
        override protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == null) { return; }
            if (((1 << other.gameObject.layer) & layersToIgnore) != 0) { return; }
            if (((1 << other.gameObject.layer) & whatIsGround) != 0)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 5f);
            }
            var enemy = other.gameObject.GetComponent<BaseAiEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(directDamage);
            }

            var result = Physics.OverlapSphere(transform.position, splashRadius, whatIsEnemy);
            foreach (var enemyInRangeCollider in result)
            {
                var enemyInRange = other.gameObject.GetComponent<BaseAiEnemy>();
                if (enemyInRange != null)
                {
                    enemyInRange.TakeDamage(splashDamage);
                }
            }

            DestroyAmmo();
        }
    }

}
