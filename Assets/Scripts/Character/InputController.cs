using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Inputs
{

    public class InputController : Singleton<InputController>
    {
        public Joystick.Joystick joystick;
        private Vector3 lastMousePositionX = new Vector3(255f, 255f, 255f);
        private Vector3 lastMousePositionY = new Vector3(255f, 255f, 255f);

        public float GetHorizontal()
        {
            if (Input.GetKey(KeyCode.A))
            {
                return -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                return 1;
            }
            if (Input.mousePosition != Vector3.zero)
            {
                var mouseMove = (Input.mousePosition - lastMousePositionX).normalized;
                Debug.Log(mouseMove);
                lastMousePositionX = Input.mousePosition;
                return mouseMove.x;
            }
            
            
            return joystick.Horizontal;
        }

        public float GetVertical()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                return -1;
            }

            if (Input.mousePosition != Vector3.zero)
            {
                var mouseMove = (Input.mousePosition - lastMousePositionY).normalized;
                lastMousePositionY = Input.mousePosition;
                return mouseMove.y;
            }
            return joystick.Vertical;
        }

        public Vector3 GetInputDirection()
        {
            return new Vector3(GetHorizontal(), 0f, GetVertical());
        }
    }

}
