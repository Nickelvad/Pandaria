using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public class CritRating : MonoBehaviour, IAttribute
    {
        public float baseValue = 5f;

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
