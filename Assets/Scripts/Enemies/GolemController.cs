using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Pandaria.Enemies
{
    public class GolemController : MonoBehaviour
    {
        public Transform character;
        public LayerMask whatIscharacter;
        public LayerMask whatIsGround;
        private NavMeshAgent navMeshAgent;
        public Animator animator;
        public float rotationSpeed = 5f;
        public float walkPointRange = 5f;
        public float viewAngle = 45f;
        public float sightRange = 10f;
        public float timeBetweenAttacks = 3f;
        public float attackRange = 3f;
        private Vector3 walkPoint;
        private bool walkPointSet;
        private bool alreadyAttacked;
        private bool characterInSightRange;
        private bool characterInAttackRange;
        private bool characterInSightAngle;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
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
                walkPointSet = true;
            }
        }
        void Patrol()
        {
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
            navMeshAgent.SetDestination(transform.position);

            if (!alreadyAttacked)
            {
                PlayAnimation("Attack01");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        void ResetAttack()
        {
            alreadyAttacked = false;
            PlayAnimation("Walk");
        }

        public float AngleInRad(Vector3 vec1, Vector3 vec2) {
            return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
        }
        public float AngleInDeg(Vector3 vec1, Vector3 vec2) {
            return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
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
