using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Inputs;

namespace Pandaria.Buildings
{
    public class TurretController : MonoBehaviour
    {
        private Vector3 rotationDirection;
        void Update()
        {
            rotationDirection = InputController.Instance.GetInputDirection();
        }

        void FixedUpdate()
        {
            // transform.RotateAround(transform.position, Vector3.up, rotationDirection.x * Time.deltaTime * 20f);
            // transform.RotateAround(transform.position, Vector3.right, rotationDirection.z * Time.deltaTime * 20f);
            transform.localEulerAngles += new Vector3(rotationDirection.z, rotationDirection.x, 0);
        }
    }

}
