using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Character.Actions;

namespace Pandaria.UI
{
    public class PickupItemButtonController : MonoBehaviour
    {
        private Button button;
        public CharacterActionController characterActionController;

        void Start()
        {
            button = GetComponentInChildren<Button>();
            SetButtonState(false);
            button.onClick.AddListener(OnClick);
            EventBus.Instance.ItemTracked += OnItemTracked;
            EventBus.Instance.ItemUntracked += OnItemUntracked;
        }

        private void SetButtonState(bool enabled_)
        {
            Debug.Log(string.Format("Setting button {0}", enabled_));
            button.gameObject.SetActive(enabled_);
        }

        private void OnItemUntracked(object sender, Item item)
        {
            SetButtonState(false);
        }

        private void OnItemTracked(object sender, Item item)
        {
            SetButtonState(true);
        }

        private void OnClick()
        {
            characterActionController.PickupItem();
        }
    }

}
