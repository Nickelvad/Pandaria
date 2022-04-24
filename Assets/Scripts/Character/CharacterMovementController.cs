using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pandaria.Buildings;

namespace Pandaria.Character
{

    public class CharacterMovementController : MonoBehaviour
    {
        public float speed = 3.0f;
        public float dashBoost = 15.0f;
        public float dashDuration = 0.5f;
        public float dashCooldawn = 0.5f;
        public float distanceToCheck = 2f;
        public float spottingRange = 5f;
        public Portal portal;

        private InputController inputController;
        private Rigidbody rigidbody_;
        private Vector3 moveDirection;
        private bool isGrounded = true;
        private RaycastHit hit;
        private GameObject spottedGameObject;
        private bool portalSpawned = false;
        private float boost = 0f;
        private float dashCooldownTimer;
        private float dashTimer;
        private bool isDashing = false;
        private Vector3 dashDirection;

        void Start()
        {
            rigidbody_ = transform.GetComponent<Rigidbody>();
            inputController = GetComponent<InputController>();
        }


        private bool GetGrounded()
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

        private void CheckAndSpawnPortal()
        {
            if (transform.position.y < -10 && !portalSpawned)
            {
                Vector3 spawnPoint = transform.position - new Vector3(0, 5, 0);
                portal.transform.position = spawnPoint;
                portal.gameObject.SetActive(true);
                portalSpawned = true;
            }
        }

        private void CheckSpottedGameObject()
        {
            spottedGameObject = CheckIfGameObjectSpotted();
            EventBus.Instance.CallGameObjectSpotted(this, spottedGameObject);
        }

        private Vector3 GetMoveDirection()
        {
            if (isDashing)
            {
                moveDirection = dashDirection;
                return moveDirection;
            }

            isGrounded = GetGrounded();
            moveDirection = inputController.GetInputDirection();
            if (!isGrounded)
            {
                moveDirection = rigidbody_.transform.forward;
            }
            moveDirection.Normalize();
            return moveDirection;
        }

        void TickDashTimers()
        {
            if (dashTimer > 0f)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer < 0f)
                {
                    dashTimer = 0f;
                }
            }

            if (dashCooldownTimer > 0f)
            {
                dashCooldownTimer -= Time.deltaTime;
                if (dashCooldownTimer < 0f)
                {
                    dashCooldownTimer = 0f;
                }
            }

        }

        void CheckDash()
        {
            bool shouldDash = Input.GetKeyDown(KeyCode.Space);
            if (dashCooldownTimer == 0 && !isDashing && shouldDash)
            {
                isDashing = true;
                dashTimer = dashDuration;
                dashDirection = moveDirection;
            }

            if (isDashing && dashTimer <= 0)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldawn;
            }
        }

        void Move()
        {
            boost = isDashing && isGrounded ? dashBoost : 0f;

            if (moveDirection != Vector3.zero)
            {
                EventBus.Instance.CallCharacterMoved(this, moveDirection);

                if (isGrounded)
                {
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                    rigidbody_.rotation = Quaternion.Slerp(rigidbody_.rotation, newRotation, Time.deltaTime * 5);
                }

                rigidbody_.MovePosition(rigidbody_.position + moveDirection * (speed + boost) * Time.deltaTime);
            }
        }

        void Update()
        {
            moveDirection = GetMoveDirection();
            TickDashTimers();
            CheckDash();
        }

        void FixedUpdate()
        {
            Move();
            CheckSpottedGameObject();
            CheckAndSpawnPortal();
        }


    }
}