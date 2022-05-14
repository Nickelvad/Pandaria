using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Buildings.Turrets
{
    public class BurningCore : BaseTurretAmmo
    {
        override protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != null && (((1 << other.gameObject.layer) & layersToIgnore) != 0))
            {
                return;
            }

            DestroyAmmo();
        }
    }

}
