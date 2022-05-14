using UnityEngine;
using UnityEngine.AI;

namespace Pandaria.Enemies
{
    public class BaseAiEnemy : MonoBehaviour
    {
        public LayerMask whatIsTarget;
        public LayerMask whatIsGround;

        public Animator animator;
        public AnimationClip idleAnimationClip;
        public AnimationClip movementAnimationClip;
        public AnimationClip attackAnimationClip;
        public AnimationClip deathAnimationClip;

        public float rotationSpeed = 5f;
        public float walkPointRange = 5f;
        public float viewAngle = 45f;
        public float sightRange = 10f;
        public float timeBetweenAttacks = 3f;
        public float attackRange = 3f;
        public float damage = 15f;
        public float hp = 100f;

        [ReadOnly] public Transform target;
        [ReadOnly] public bool walkPointSet = false;
        [ReadOnly] public bool isAttacking = false;
        [ReadOnly] public bool alreadyAttacked = false;
        [ReadOnly] public bool targetInSightRange = false;
        [ReadOnly] public bool targetInAttackRange = false;
        [ReadOnly] public bool targetInSightAngle = false;
        protected Vector3 walkPoint;
        protected NavMeshAgent navMeshAgent;

        virtual protected void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        virtual public void Update()
        {
            if (isAttacking)
            {
                return;
            }

            targetInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsTarget);
            targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsTarget);
            targetInSightAngle = CheckIfInSightAngle();

            if (!targetInAttackRange && !targetInSightRange) 
            {
                Patrol();
            }

            if (!targetInAttackRange && targetInSightRange)
            {
                Chase();
                RotateTowardsTarget();
            }

            if (targetInAttackRange && targetInSightRange && !targetInSightAngle)
            {
                RotateTowardsTarget();
            }

            if (targetInAttackRange && targetInSightRange && targetInSightAngle)
            {
                Attack();
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
                PlayAnimation(movementAnimationClip.name);
            }

            Vector3 distanceToWalkpoint = transform.position - walkPoint;

            if (distanceToWalkpoint.magnitude < 1f)
            {
                walkPointSet = false;
                PlayAnimation(idleAnimationClip.name);
            }
        }

        virtual protected void Chase()
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }

        virtual protected void RotateTowardsTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        virtual protected void Attack()
        {

        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            if (hp < 0)
            {
                PlayAnimation(deathAnimationClip.name);
                Destroy(this.gameObject, deathAnimationClip.length);
            }
        }
   
        virtual protected bool CheckIfInSightAngle()
        {
            Vector3 direction = target.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);
            return angle <= viewAngle;
        }

        virtual protected void SearchWalkPoint()
        {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

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

        virtual protected void PlayAnimation(string name)
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName(name))
            {
                animator.Play(name, 0);
            }
        }

        virtual protected void SetVisible(bool shouldBeVisible)
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = shouldBeVisible;
            }
        }

        virtual protected void ResetAttack()
        {
            alreadyAttacked = false;
            PlayAnimation(movementAnimationClip.name);
        }

        virtual protected void FinishAttack()
        {
            isAttacking = false;
        }

    }

}
