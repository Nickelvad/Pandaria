using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Characters.Actions;

namespace Pandaria.UI
{
    public class PickupItemButtonController : MonoBehaviour, IButtonAction
    {
        public CharacterPickupController characterPickupController;

        public bool isActive { get; private set; }

        void Start()
        {
            EventBus.Instance.ItemContainerTracked += OnItemContainerTracked;
            EventBus.Instance.ItemContainerUntracked += OnItemContainerUntracked;
        }

        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnItemContainerUntracked(object sender, ItemContainer itemContainer)
        {
            SetButtonState(false);
        }

        private void OnItemContainerTracked(object sender, ItemContainer itemContainer)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterPickupController.PickupItem();
        }
    }

}
