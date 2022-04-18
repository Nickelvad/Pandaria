using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pandaria.Joystick;

public class CharacterMovementController : MonoBehaviour
{
    public float inputX = 0.0f;
    public float inputY = 0.0f;
    private Vector3 moveDirection;
    public float speed = 3.0f;
    public Joystick joystick;

    public float jumpSpeed = 2.0f;

    private Rigidbody rigidbody_;

    public float distanceToCheck = 2f;
    public bool isGrounded = true;
    private bool oldIsGrouned = true;
    private bool startedToJump = false;
    private Vector3 jumpDirection;
    private RaycastHit hit;

    void Start()
    {
        rigidbody_ = transform.GetComponent<Rigidbody>();
    }

    private float GetHorizontal()
    {
        return joystick.Horizontal;
    }

    private float GetVertical()
    {
        return joystick.Vertical;
    }

    private bool CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, out hit, distanceToCheck);
    }

    void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();

        inputY = this.GetVertical();
        inputX = this.GetHorizontal();



        moveDirection = new Vector3(inputX, 0f, inputY);
        if (!isGrounded)
        {
            moveDirection = rigidbody_.transform.forward;
        }
        moveDirection.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            startedToJump = true;
            rigidbody_.AddForce(Vector2.up * jumpSpeed, ForceMode.Impulse);
        }

        if (moveDirection != Vector3.zero)
        {
            if (isGrounded)
            {
                Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                rigidbody_.rotation = Quaternion.Slerp(rigidbody_.rotation, newRotation, Time.deltaTime * 5);
            }

            rigidbody_.MovePosition(rigidbody_.position + moveDirection * speed * Time.deltaTime);
            
        }
    }
}
