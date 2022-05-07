using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria.Gatherables;

namespace Pandaria.UI.Actions
{
    public class LootButtonController : MonoBehaviour, IButtonAction
{
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
            SetButtonState(false);
        }

        private void OnLootContainerUntracked(object sender, LootContainer lootContainer)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
        }
}

}
