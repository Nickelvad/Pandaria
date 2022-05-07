using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Characters.Actions;

namespace Pandaria.UI.Actions
{
    public class PickupItemButtonController : MonoBehaviour, IButtonAction
    {
        public CharacterPickupController characterPickupController;

        public bool isActive { get; private set; }

        void Awake()
        {
            EventBus.Instance.ItemContainerTracked += OnItemContainerTracked;
            EventBus.Instance.ItemContainerUntracked += OnItemContainerUntracked;
        }

        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnItemContainerUntracked(object sender, PickableItemContainer itemContainer)
        {
            SetButtonState(false);
        }

        private void OnItemContainerTracked(object sender, PickableItemContainer itemContainer)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterPickupController.PickupItem();
        }
    }

}
