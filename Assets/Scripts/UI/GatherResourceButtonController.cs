using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pandaria;
using Pandaria.Items;
using Pandaria.Characters.Actions;

namespace Pandaria.UI
{
    public class GatherResourceButtonController : MonoBehaviour, IButtonAction
    {

        public CharacterGatheringController characterGatheringController;
        public bool isActive { get; private set; }

        void Start()
        {
            EventBus.Instance.GatherableResourceTracked += OnGatherableResourceTracked;
            EventBus.Instance.GatherableResourceUntracked += OnGatherableResourceUntracked;
        }

        private void SetButtonState(bool enabled_)
        {
            isActive = enabled_;
        }

        private void OnGatherableResourceUntracked(object sender, GatherableResource gatherableResource)
        {
            SetButtonState(false);
        }

        private void OnGatherableResourceTracked(object sender, GatherableResource gatherableResource)
        {
            SetButtonState(true);
        }

        public void OnClick()
        {
            characterGatheringController.DoAction();
        }
    }

}
