using UnityEngine;
using Pandaria.Cameras;

namespace Pandaria.Buildings
{
    public class Tower : MonoBehaviour
    {
        public GameObject turret;
        public GameObject character;
        public Camera characterCamera;
        public Camera turretCamera;


        public void Activate()
        {
            character.SetActive(false);
            characterCamera.enabled = false;
            turretCamera.enabled = true;
        }
    }
}

