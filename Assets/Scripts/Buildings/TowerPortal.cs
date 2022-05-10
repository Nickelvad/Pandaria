using UnityEngine;
using Pandaria.Characters;

namespace Pandaria.Buildings
{
    public class TowerPortal : MonoBehaviour
    {
        public LayerMask whatIsCharacter;
        public Tower tower;
        void OnTriggerEnter(Collider other)
        {

            if (other.gameObject != null && (((1 << other.gameObject.layer) & whatIsCharacter) != 0))
            {
                tower.Activate();
            }
        }
    }
}

