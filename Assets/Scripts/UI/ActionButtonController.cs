using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Character.Actions;

namespace Pandaria.UI
{
    public class ActionButtonController : MonoBehaviour
    {
        private Button button;
        public CharacterActionController characterActionController;
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnClick()
        {
            characterActionController.DoAction(null);
        }
    }

}
