using UnityEngine;

namespace Pandaria.Characters
{
    public class CharacterController : MonoBehaviour
    {
        public int hp = 100;
        public int stamina = 100;
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
