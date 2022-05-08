using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public class Mana : MonoBehaviour, IAttribute
    {
        public float baseValue = 100f;

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
