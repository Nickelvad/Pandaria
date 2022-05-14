using System;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Characters.Attributes;
using Pandaria.Buildings;

namespace Pandaria.Enemies
{
    public class GolemController : BaseAiEnemy, IColliderListener
    {
        public Transform character;
        public List<ColliderBridge> extraColliders;

        override protected void Awake()
        {
            base.Awake();
            target = character;
            foreach (ColliderBridge colliderBridge in extraColliders)
            {
                colliderBridge.Initialize(this);
            }
            EventBus.Instance.NightStarted += (object sender, EventArgs e) => SetVisible(false);
            EventBus.Instance.DayStarted += (object sender, EventArgs e) => SetVisible(true);
            
        }

        override protected void Attack()
        {
            navMeshAgent.isStopped = true;
            if (!alreadyAttacked)
            {
                PlayAnimation(attackAnimationClip.name);
                alreadyAttacked = true;
                isAttacking = true;
                Invoke(nameof(FinishAttack), attackAnimationClip.length);
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        public void ExtraOnCollisionEnter(GameObject notifier, Collision collision)
        {
            CharacterAttributesController characterAttributeController = collision.rigidbody.GetComponent<CharacterAttributesController>();
            if (characterAttributeController != null)
            {
                characterAttributeController.ApplyDamage(damage);
            }
        }
     
       public void ExtraOnTriggerEnter(GameObject notifier, Collider other)
        {
            CharacterAttributesController characterAttributeController = other.GetComponent<CharacterAttributesController>();
            if (characterAttributeController != null && isAttacking && notifier.name == "Index_Proximal_R")
            {
                characterAttributeController.ApplyDamage(damage);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Crystal crystal = other.GetComponent<Crystal>();
            if (crystal != null)
            {
                SetVisible(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            Crystal crystal = other.GetComponent<Crystal>();
            if (crystal != null)
            {
                SetVisible(false);
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
