using UnityEngine;
using Pandaria.Buildings.Turrets;

namespace Pandaria.Buildings
{
    public class Tower : MonoBehaviour
    {
        public GameObject turret;
        public GameObject character;
        public Camera characterCamera;
        public Camera turretCamera;
        private TurretController turretController;

        void Awake()
        {
            turretController = GetComponentInChildren<TurretController>();
            turretController.enabled = false;
        }

        public void Activate()
        {
            character.SetActive(false);
            turretCamera.enabled = true;
            turretCamera.gameObject.SetActive(true);

            characterCamera.enabled = false;
            characterCamera.gameObject.SetActive(false);
            turretController.enabled = true;

        }
    }
}

