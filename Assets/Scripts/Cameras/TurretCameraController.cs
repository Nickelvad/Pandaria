using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Cameras
{
    public class TurretCameraController : MonoBehaviour
    {
        public GameObject turretFocusPoint;
        public float verticalAngle = 190f;
        public float distance = 3f;

        void Update()
        {
            if (turretFocusPoint == null)
            {
                return;
            }

            var vector = Quaternion.AngleAxis(verticalAngle, turretFocusPoint.transform.right) * turretFocusPoint.transform.forward;
            var result = turretFocusPoint.transform.position + vector * distance;
            transform.position = Vector3.Slerp(transform.position, result, 5f);
            transform.LookAt(turretFocusPoint.transform);
        }
    }

}
