using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Character.Actions;

namespace Pandaria.UI
{
    public class PickupItemButtonController : MonoBehaviour, IButtonAction
    {
        private Button button;
        public CharacterPickupController characterPickupController;

        public bool isActive { get; private set; }

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
            isActive = enabled_;
        }

        private void OnItemUntracked(object sender, Item item)
        {
            SetButtonState(false);
        }

        private void OnItemTracked(object sender, Item item)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterPickupController.PickupItem();
        }
    }

}
