using UnityEngine;

namespace Pandaria.Characters
{
    public class CharacterStatusController : MonoBehaviour
    {
        public float hp = 100f;
        public float stamina = 100f;
        public float mana = 0f;
        [ReadOnly] public float defence = 0f;
        [ReadOnly] public float critDefenceRating = 5.0f;
        [ReadOnly] public float attackMin = 0f;
        [ReadOnly] public float attackMax = 0f;
        [ReadOnly] public float attackSpeed = 5.0f;
        [ReadOnly] public float critRating = 5.0f;
        public float currentHp = 100f;
        public float currentStamina = 100f;
        public float hpRecoverySpeed = 0f;
        public float staminaRecoverySpeed = 10f;
        public int dashStaminaPrice = 30;

        public bool canDash()
        {
            return currentStamina >= dashStaminaPrice;
        }

        public void ApplyDash()
        {
            currentStamina -= dashStaminaPrice;
            EventBus.Instance.CallCharacterStaminaChanged(this, (int)Mathf.Round(currentStamina));
        }

        public void ApplyDamage(float damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                currentHp = 0;
                Debug.Log("Death");
            }
            EventBus.Instance.CallCharacterHpChanged(this, (int)Mathf.Round(currentHp));
        }


        void Update()
        {
            if (currentStamina < stamina)
            {
                currentStamina += staminaRecoverySpeed * Time.deltaTime;
                EventBus.Instance.CallCharacterStaminaChanged(this, (int)Mathf.Round(currentStamina));
            } 
        }
    }

}
