using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Characters.Attributes
{
    public interface IAttribute
    {
        public float currentValue { get; set; }

        public float maxValue { get; set; }

        public void Recalculate();
    }

}
