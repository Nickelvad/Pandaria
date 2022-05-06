using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pandaria.Characters;
namespace Pandaria.UI.Character
{
    public class CharacterWindowController : MonoBehaviour
    {
        public Button closeButton;
        public CharacterStatusController characterStatusController;
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

        void OnEnable()
        {
            UpdateStats();
        }


        public void CloseClick()
        {
            gameObject.SetActive(false);
        }
    }

}
