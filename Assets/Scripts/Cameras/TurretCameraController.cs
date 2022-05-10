using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Cameras
{
    public class TurretCameraController : MonoBehaviour
    {
        public GameObject turret;
        public float verticalAngle = 190f;
        public float distance = 3f;

        void Update()
        {
            if (turret == null)
            {
                return;
            }

            var vector = Quaternion.AngleAxis(verticalAngle, turret.transform.right) * turret.transform.forward;
            var result = turret.transform.position + vector * distance;
            transform.position = Vector3.Slerp(transform.position, result, 5f);
            transform.LookAt(turret.transform);
        }
    }

}
