using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pandaria.UI.Actions
    {
    public class ActionButtonController : MonoBehaviour
    {
        public Button actionButton;
        private List<IButtonAction> buttonActions;
        private IButtonAction activeButtonAction;

        void Start()
        {
            buttonActions = new List<IButtonAction>();
            buttonActions.AddRange(GetComponents<IButtonAction>());

            actionButton.onClick.AddListener(OnClick);
        }

        void LateUpdate()
        {
            activeButtonAction = null;
            foreach (IButtonAction buttonAction in buttonActions)
            {
                if (buttonAction.isActive)
                {
                    activeButtonAction = buttonAction;
                    break;
                }
            }
            actionButton.gameObject.SetActive(activeButtonAction != null);
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                OnClick();
            }
        }

        void OnClick()
        {
            if (activeButtonAction != null)
            {
                activeButtonAction.OnClick();
            }
        }
    }

}
