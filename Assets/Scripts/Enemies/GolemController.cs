using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pandaria.Characters;

namespace Pandaria.Enemies
{
    public class GolemController : MonoBehaviour, IColliderListener
    {
        public Transform character;
        public LayerMask whatIscharacter;
        public LayerMask whatIsGround;
        private NavMeshAgent navMeshAgent;
        public Animator animator;
        public List<ColliderBridge> extraColliders;
        public float rotationSpeed = 5f;
        public float walkPointRange = 5f;
        public float viewAngle = 45f;
        public float sightRange = 10f;
        public float timeBetweenAttacks = 3f;
        public float attackRange = 3f;
        public float damage = 15f;
        private Vector3 walkPoint;
        public bool walkPointSet;
        public bool alreadyAttacked;
        public bool characterInSightRange;
        public bool characterInAttackRange;
        public bool characterInSightAngle;
        public bool isAttacking = false;
        public AnimationClip attackAnimationClip;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            foreach (ColliderBridge colliderBridge in extraColliders)
            {
                colliderBridge.Initialize(this);
            }
            
        }

        public void Update()
        {
            if (isAttacking)
            {
                return;
            }

            characterInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIscharacter);
            characterInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIscharacter);
            characterInSightAngle = CheckIfInSightAngle();

            if (!characterInAttackRange && !characterInSightRange) 
            {
                Patrol();
            }

            if (!characterInAttackRange && characterInSightRange)
            {
                Chase();
            }

            if (characterInAttackRange && characterInSightRange && !characterInSightAngle)
            {
                Rotate();
            }

            if (characterInAttackRange && characterInSightRange && characterInSightAngle)
            {
                Attack();
            }
        }

        bool CheckIfInSightAngle()
        {
            Vector3 direction = character.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);
            return angle <= viewAngle;
        }

        void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            
            
            if (Physics.Raycast(walkPoint, -transform.up, 3f, whatIsGround))
            {
                NavMeshPath path = new NavMeshPath();
                if (navMeshAgent.CalculatePath(walkPoint, path))
                {
                    walkPointSet = true;
                }
                
            }
        }
        void Patrol()
        {
            navMeshAgent.isStopped = false;
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }

            if (walkPointSet)
            {
                navMeshAgent.SetDestination(walkPoint);
                transform.LookAt(navMeshAgent.destination);
                PlayAnimation("Walk");
            }

            Vector3 distanceToWalkpoint = transform.position - walkPoint;

            if (distanceToWalkpoint.magnitude < 1f)
            {
                walkPointSet = false;
                PlayAnimation("Idle");
            }
        }

        void PlayAnimation(string name)
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName(name))
            {
                animator.Play(name, 0);
            }
        }

        void Chase()
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(character.position);
        }

        void Rotate()
        {
            Vector3 direction = (character.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        void Attack()
        {
            navMeshAgent.isStopped = true;
            if (!alreadyAttacked)
            {
                PlayAnimation("Attack01");
                alreadyAttacked = true;
                isAttacking = true;
                Invoke(nameof(FinishAttack), attackAnimationClip.length);
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        void ResetAttack()
        {
            alreadyAttacked = false;
            PlayAnimation("Walk");
        }

        void FinishAttack()
        {
            isAttacking = false;
        }


        public void ExtraOnCollisionEnter(GameObject notifier, Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            Characters.CharacterController character = collision.rigidbody.gameObject.GetComponent<Characters.CharacterController>();
            if (character != null)
            {
                character.ApplyDamage(damage);
            }
        }
        public void ExtraOnTriggerEnter(GameObject notifier, Collider other)
        {
            Characters.CharacterController character = other.gameObject.GetComponent<Characters.CharacterController>();
            if (character != null && isAttacking && notifier.name == "Index_Proximal_R")
            {
                Debug.Log(gameObject.name);
                character.ApplyDamage(damage);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 sight = transform.position + transform.forward * 5;
            Vector3 leftSight = Quaternion.Euler(0, -45, 0) * (sight - transform.position);
            Vector3 rightSight = Quaternion.Euler(0, 45, 0) * (sight - transform.position);

            Gizmos.DrawLine(transform.position, sight);
            Gizmos.DrawLine(transform.position, transform.position + leftSight);
            Gizmos.DrawLine(transform.position, transform.position + rightSight);
        }
    }

}
