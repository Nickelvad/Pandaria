using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Cameras
{
    public class TurretCameraController : MonoBehaviour
    {
        public GameObject turret;
        public float verticalAngle = 3f;
        public float distance = 3f;

        void Update()
        {
            if (turret == null)
            {
                return;
            }

            transform.position = turret.transform.position - new Vector3(distance, 0, distance);
            // transform.position += new Vector3(0, verticalAngle, 0);
            // transform.RotateAround(transform.position, transform.right, verticalAngle);
            transform.LookAt(turret.transform);
        }
    }

}
