using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Characters
{

    public class InputController : MonoBehaviour
    {
        public Joystick.Joystick joystick;

        private float GetHorizontal()
        {
            if (Input.GetKey(KeyCode.A))
            {
                return -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                return 1;
            }
            return joystick.Horizontal;
        }

        private float GetVertical()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                return -1;
            }
            return joystick.Vertical;
        }

        public Vector3 GetInputDirection()
        {
            return new Vector3(GetHorizontal(), 0f, GetVertical());
        }
    }

}
