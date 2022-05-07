using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pandaria.Characters;
using Pandaria.Characters.Inventory;
using Pandaria.UI.Inventory;
using Pandaria.Items;

namespace Pandaria.UI.Character
{
    public class CharacterWindowController : MonoBehaviour
    {
        public Button closeButton;
        public CharacterStatusController characterStatusController;
        public CharacterInventoryController characterInventoryController;
        public CharacterSlotController helmSlot;
        public CharacterSlotController shouldersSlot;
        public CharacterSlotController chestSlot;
        public CharacterSlotController armsSlot;
        public CharacterSlotController weaponPrimarySlot;
        public CharacterSlotController legsSlot;
        public CharacterSlotController bootsSlot;
        public CharacterSlotController necklaceSlot;
        public CharacterSlotController ringSlot;
        public CharacterSlotController weaponSecondarySlot;
        public TextMeshProUGUI healthValueText;
        public TextMeshProUGUI staminaValueText;
        public TextMeshProUGUI manaValueText;
        public TextMeshProUGUI defenceValueText;
        public TextMeshProUGUI critDefenceRatingValueText;
        public TextMeshProUGUI attackValueText;
        public TextMeshProUGUI attackSpeedValueText;    
        public TextMeshProUGUI critRatingValueText;

        void Awake()
        {
            closeButton.onClick.AddListener(CloseClick);
        }

        private void UpdateStats()
        {
            healthValueText.text = characterStatusController.hp.ToString();
            staminaValueText.text = characterStatusController.stamina.ToString();
            manaValueText.text = characterStatusController.mana.ToString();
            defenceValueText.text = characterStatusController.defence.ToString();
            critDefenceRatingValueText.text = characterStatusController.critDefenceRating.ToString();
            attackValueText.text = string.Format(
                "{0} - {1}", characterStatusController.attackMin.ToString(), characterStatusController.attackMax.ToString()
            );
        }

        private void UpdateSlots()
        {
           InitializeSlot(helmSlot, characterInventoryController.equipedArmor[ArmorSlot.Helm].armor);
           InitializeSlot(shouldersSlot, characterInventoryController.equipedArmor[ArmorSlot.Shoulders].armor);
           InitializeSlot(chestSlot, characterInventoryController.equipedArmor[ArmorSlot.Chest].armor);
           InitializeSlot(armsSlot, characterInventoryController.equipedArmor[ArmorSlot.Arms].armor);
           InitializeSlot(legsSlot, characterInventoryController.equipedArmor[ArmorSlot.Legs].armor);
           InitializeSlot(bootsSlot, characterInventoryController.equipedArmor[ArmorSlot.Boots].armor);
           InitializeSlot(necklaceSlot, characterInventoryController.equipedArmor[ArmorSlot.Necklace].armor);
           InitializeSlot(ringSlot, characterInventoryController.equipedArmor[ArmorSlot.Ring].armor);
           InitializeSlot(weaponPrimarySlot, characterInventoryController.equipedWeapons[WeaponSlot.Primary].weapon);
           InitializeSlot(weaponSecondarySlot, characterInventoryController.equipedWeapons[WeaponSlot.Secondary].weapon);
        } 

        private void InitializeSlot(CharacterSlotController slot, EquipableItem item)
        {
            slot.Initialize(item);
        }

        void OnEnable()
        {
            UpdateStats();
            UpdateSlots();
        }


        public void CloseClick()
        {
            gameObject.SetActive(false);
        }
    }

}
