using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public class Health : MonoBehaviour, IAttribute
    {
        public float baseValue = 100f;

        public float currentValue { get ; set; }
        public float maxValue { get ; set; }

        void Awake()
        {
            currentValue = baseValue;
            maxValue = baseValue;
        }
        
        public void ApplyDamage(float damage)
        {
            currentValue -= damage;
            if (currentValue <= 0)
            {
                currentValue = 0;
                Debug.Log("Death");
            }
            EventBus.Instance.CallCharacterHpChanged(this, (int)Mathf.Round(currentValue));
        }

        public void Recalculate()
        {
            currentValue = baseValue;
            maxValue = baseValue;
        }
    }

}
