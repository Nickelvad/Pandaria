using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.UI
{
    public interface IButtonAction 
    {
        // Start is called before the first frame update
        public bool isActive { get; }
        public void OnClick();

    }
}

