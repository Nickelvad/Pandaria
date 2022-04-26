using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Characters.Actions;

namespace Pandaria.UI
{
    public class DropItemButtonController : MonoBehaviour, IButtonAction
    {
        private Button button;
        public bool isActive { get; private set; }
        public CharacterPickupController characterPickupController;

        void Start()
        {
            button = GetComponentInChildren<Button>();
            SetButtonState(false);
            button.onClick.AddListener(OnClick);
            EventBus.Instance.ItemPickedup += OnItemPickedup;
            EventBus.Instance.ItemDropped += OnItemDropped;
        }

        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnItemDropped(object sender, Item item)
        {
            Debug.Log(sender.ToString());
            SetButtonState(false);
        }

        private void OnItemPickedup(object sender, Item item)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterPickupController.DropItem();
        }
    }

}
