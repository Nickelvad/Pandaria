using UnityEngine;
using Pandaria.Characters.Actions;
using Pandaria.Gatherables;
using Pandaria.UI.LootContainers;

namespace Pandaria.UI.Actions
{
    public class LootButtonController : MonoBehaviour, IButtonAction
{
        public CharacterLootController characterLootController;
        public GameObject lootContainerWindow;
        public bool isActive { get; private set; }

        void Awake()
        {
            EventBus.Instance.LootContainerTracked += OnLootContainerTracked;
            EventBus.Instance.LootContainerUntracked += OnLootContainerUntracked;
        }


        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnLootContainerTracked(object sender, LootContainer lootContainer)
        {
            SetButtonState(true);
        }

        private void OnLootContainerUntracked(object sender, LootContainer lootContainer)
        {
            SetButtonState(false);
        }

        public void OnClick()
        {
            lootContainerWindow.SetActive(true);
            LootContainerWindowController lootContainerWindowController = lootContainerWindow.GetComponent<LootContainerWindowController>();
            lootContainerWindowController.Initialize(characterLootController.trackedLootContainer);
        }
}

}
