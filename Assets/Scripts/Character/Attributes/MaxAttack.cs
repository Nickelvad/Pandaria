using UnityEngine;
using Pandaria.Characters.Inventory;

namespace Pandaria.Characters.Attributes
{
    public class MaxAttack : MonoBehaviour, IAttribute
    {
        public float baseValue = 1f;

        public float currentValue { get ; set; }
        public float maxValue { get ; set; }

        void Awake()
        {
            currentValue = baseValue;
            maxValue = baseValue;
        }

        void Start()
        {
            Recalculate();
        }

        public void Recalculate()
        {
            float newValue = baseValue;
            foreach (var slot in CharacterInventoryController.Instance.equipedWeapons)
            {
                if (slot.Value.weapon != null)
                {
                    newValue += slot.Value.weapon.maxAttack;
                }
                
            }

            currentValue = newValue;
            maxValue = newValue;
            EventBus.Instance.CallAttributeChanged(this, this);
        }
    }

}
