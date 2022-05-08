using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public class MinAttack : MonoBehaviour, IAttribute
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
            currentValue = baseValue;
            maxValue = baseValue;
        }
    }

}
