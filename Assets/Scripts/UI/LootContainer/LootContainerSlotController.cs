using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Gatherables;

namespace Pandaria.UI.LootContainers
{
    public class LootContainerSlotController : MonoBehaviour
    {
        private LootContent lootContent;
        public Image slotImage;
        public Text text;
        public void Initialize(LootContent lootContent)
        {
            this.lootContent = lootContent;
            if (lootContent.item != null)
            {
                slotImage.enabled = true;
                slotImage.sprite = lootContent.item.inventoryImage;
                text.text = lootContent.number.ToString();
                text.enabled = true;
            }
            else
            {
                slotImage.enabled = false;
                text.enabled = false;
            }
            
        }
    }
}
