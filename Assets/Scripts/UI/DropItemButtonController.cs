using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;
using Pandaria.Character.Actions;

namespace Pandaria.UI
{
    public class DropItemButtonController : MonoBehaviour
    {
        private Button button;
        public CharacterActionController characterActionController;

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
            Debug.Log(string.Format("Setting drop button {0}", enabled_.ToString()));
            button.gameObject.SetActive(enabled_);
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

        private void OnClick()
        {
            characterActionController.DropItem();
        }
    }

}
