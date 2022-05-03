using UnityEngine;

namespace Pandaria.Characters
{

    public class CharacterMovementController : MonoBehaviour
    {
        public Character character;
        public float speed = 3.0f;
        public float dashDistance = 8.0f;
        public float dashMultiplier = 2.0f;
        public float dashCooldawn = 0.5f;
        public float dashDuration = 0.5f;
        public float jumpHeight = 5f;
        public float distanceToCheck = 0.2f;
        public float spottingRange = 5f;
        public Transform groundChecker;
        public LayerMask whatIsGround;

        private InputController inputController;
        private Rigidbody rigidbody_;
        private Vector3 moveDirection;
        public bool isGrounded = true;
        private RaycastHit hit;
        private GameObject spottedGameObject;
        private bool isDashing = false;
        private bool dashIsOnCooldawn = false;
        private Vector3 dashDirection;

        void Start()
        {
            rigidbody_ = transform.GetComponent<Rigidbody>();
            inputController = GetComponent<InputController>();
        }


        private bool GetGrounded()
        {
            return Physics.CheckSphere(groundChecker.position, distanceToCheck, whatIsGround, QueryTriggerInteraction.Ignore);
        }

        private GameObject GetSpottedGameObject()
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, spottingRange))
            {
                return hit.transform.gameObject;
            }
            return null;
        }

        private void CheckSpottedGameObject()
        {
            spottedGameObject = GetSpottedGameObject();
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

        void Dash()
        {
            bool shouldDash = Input.GetButtonDown("Dash");
            if (!dashIsOnCooldawn && !isDashing && shouldDash && character.canDash())
            {
                isDashing = true;
                dashIsOnCooldawn = true;
                dashDirection = moveDirection;

                Vector3 dashVelocity = Vector3.Scale(
                    transform.forward, dashDistance * new Vector3(
                        (Mathf.Log(1f / (Time.deltaTime * dashMultiplier + 1)) / -Time.deltaTime),
                        0,
                        (Mathf.Log(1f / (Time.deltaTime * dashMultiplier + 1)) / -Time.deltaTime)
                    )
                );
                rigidbody_.AddForce(dashVelocity, ForceMode.VelocityChange);
                character.ApplyDash();
                Invoke(nameof(ResetIsDashing), dashDuration);
                Invoke(nameof(ResetDashCooldawn), dashCooldawn);
            }
        }

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rigidbody_.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
        }

        void ResetDashCooldawn()
        {
            dashIsOnCooldawn = false;
        }

        void ResetIsDashing()
        {
            isDashing = false;
        }

        void Move()
        {
            if (moveDirection != Vector3.zero)
            {
                EventBus.Instance.CallCharacterMoved(this, moveDirection);

                if (isGrounded)
                {
                    Quaternion newRotation = Quaternion.LookRotation(moveDirection);
                    rigidbody_.rotation = Quaternion.Slerp(rigidbody_.rotation, newRotation, Time.deltaTime * 5);
                }
                rigidbody_.MovePosition(rigidbody_.position + moveDirection * speed * Time.deltaTime);
            }
        }

        void Update()
        {
            moveDirection = GetMoveDirection();
            if (moveDirection != Vector3.zero)
            {
                transform.forward = moveDirection;
            }
            Dash();
            Jump();
        }

        void FixedUpdate()
        {
            Move();
            CheckSpottedGameObject();
        }

    }
}