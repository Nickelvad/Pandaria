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
            EventBus.Instance.ItemContainerPickedup += OnItemContainerPickedup;
            EventBus.Instance.ItemContainerDropped += OnItemContainerDropped;
        }

        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnItemContainerDropped(object sender, PickableItemContainer itemContainer)
        {
            SetButtonState(false);
        }

        private void OnItemContainerPickedup(object sender, PickableItemContainer itemContainer)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterPickupController.DropItemContainer();
        }
    }

}
