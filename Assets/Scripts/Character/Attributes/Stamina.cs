using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public class Stamina : MonoBehaviour, IAttribute
    {
        public float baseValue = 100f;
        public float staminaRecoverySecond = 2f;
        public float dashStaminaPrice = 10f;
        public float currentValue { get ; set; }
        public float maxValue { get ; set; }

        void Awake()
        {
            currentValue = baseValue;
            maxValue = baseValue;
            InvokeRepeating(nameof(UpdateStamina), 1, 1);
        }
        
        public bool CanDash()
        {
            return currentValue >= dashStaminaPrice;
        }

        public void ApplyDash()
        {
            currentValue -= dashStaminaPrice;
            EventBus.Instance.CallCharacterStaminaChanged(this, (int)Mathf.Round(currentValue));
        }

        private void UpdateStamina()
        {
            currentValue += staminaRecoverySecond;
            if (currentValue > maxValue)
            {
                currentValue = maxValue;
            }
            EventBus.Instance.CallCharacterStaminaChanged(this, (int)Mathf.Round(currentValue));
        }

        void OnDisable()
        {
            CancelInvoke(nameof(UpdateStamina));
        }

        public void Recalculate()
        {
            currentValue = baseValue;
            maxValue = baseValue;
        }
    }

}
