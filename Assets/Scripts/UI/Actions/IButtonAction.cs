using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.UI.Actions
{
    public interface IButtonAction 
    {
        public bool isActive { get; }
        public void OnClick();

    }
}

