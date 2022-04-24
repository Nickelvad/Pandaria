using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pandaria;
using Pandaria.Joystick;
using Pandaria.Buildings;

public class CharacterMovementController : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 3.0f;
    public float jumpSpeed = 2.0f;
    public float distanceToCheck = 2f;
    public float spottingRange = 5f;
    public Portal portal;
    private Rigidbody rigidbody_;
    private float inputX = 0.0f;
    private float inputY = 0.0f;
    private Vector3 moveDirection;
    private bool isGrounded = true;
    private RaycastHit hit;
    private GameObject spottedGameObject;
    private bool portalSpawned = false;

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

    private GameObject CheckIfGameObjectSpotted()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, spottingRange))
        {
            return hit.transform.gameObject;
        }
        return null;
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
        
        spottedGameObject = CheckIfGameObjectSpotted();
        EventBus.Instance.CallGameObjectSpotted(this, spottedGameObject);

        if (transform.position.y < -10 && !portalSpawned)
        {
            Debug.Log("SPAWNING");
            Vector3 spawnPoint = transform.position - new Vector3(0, 5, 0);
            portal.transform.position = spawnPoint;
            portal.gameObject.SetActive(true);
            portalSpawned = true;
        }
    }

}
