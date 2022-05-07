using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pandaria.Items;

namespace Pandaria.UI.Character
{
    public class CharacterSlotController : MonoBehaviour
    {
        public EquipableItem equipableItem;
        public Image inventorySlotBackgroundImage;
        public Image inventorySlotImage;
        public void Initialize(EquipableItem equipableItem)
        {
            Debug.Log(equipableItem);
            this.equipableItem = equipableItem;
            if (equipableItem != null)
            {
                Debug.Log("Setting image");
                inventorySlotImage.enabled = true;
                inventorySlotImage.sprite = equipableItem.inventoryImage;
            }
            else
            {
                inventorySlotImage.enabled = false;
            }
            
        }
    }

}
