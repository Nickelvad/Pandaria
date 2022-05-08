using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pandaria.Characters.Inventory;
using Pandaria.Characters.Attributes;
using Pandaria.Items;

namespace Pandaria.UI.Character
{
    public class CharacterWindowController : MonoBehaviour
    {
        public Button closeButton;
        public CharacterAttributesController characterStatusController;
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
        private Dictionary<IAttribute, TextMeshProUGUI> values;

        void Awake()
        {
            closeButton.onClick.AddListener(CloseClick);
            EventBus.Instance.EquipmentChanged += OnEquipmentChanged;

            values = new Dictionary<IAttribute, TextMeshProUGUI>
            {
                {CharacterAttributesController.Instance.health, healthValueText},
                {CharacterAttributesController.Instance.stamina, staminaValueText},
                {CharacterAttributesController.Instance.mana, manaValueText},
                {CharacterAttributesController.Instance.defence, defenceValueText},
                {CharacterAttributesController.Instance.critDefenceRating, critDefenceRatingValueText},
                {CharacterAttributesController.Instance.attackSpeed, attackSpeedValueText},
                {CharacterAttributesController.Instance.critRating, critRatingValueText},
            };
        }

        private void UpdateStats()
        {
            healthValueText.text = characterStatusController.health.maxValue.ToString();
            staminaValueText.text = characterStatusController.stamina.maxValue.ToString();
            manaValueText.text = characterStatusController.mana.maxValue.ToString();
            defenceValueText.text = characterStatusController.defence.currentValue.ToString();
            critDefenceRatingValueText.text = characterStatusController.critDefenceRating.currentValue.ToString();
            attackValueText.text = string.Format(
                "{0} - {1}",
                characterStatusController.minAttack.currentValue.ToString(),
                characterStatusController.maxAttack.currentValue.ToString()
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

        public void OnEquipmentChanged(object sender, EquipableItem equipableItem)
        {
            UpdateSlots();
            UpdateStats();
        }

        public void CloseClick()
        {
            gameObject.SetActive(false);
        }

        void OnAttributeChanged(object sender, IAttribute attribute)
        {
            if (attribute is MinAttack minAttack)
            {
                attackValueText.text = string.Format(
                    "{0} - {1}",
                    minAttack.currentValue.ToString(),
                    characterStatusController.maxAttack.currentValue.ToString()
                );
                return;
            }

            if (attribute is MaxAttack maxAttack)
            {
                attackValueText.text = string.Format(
                    "{0} - {1}",
                    CharacterAttributesController.Instance.minAttack.currentValue.ToString(),
                    maxAttack.currentValue.ToString()
                );
                return;
            }

            var text = values[attribute];
            text.text = attribute.currentValue.ToString();
        }
    }

}
