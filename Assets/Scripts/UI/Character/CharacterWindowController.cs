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


        void Awake()
        {
            closeButton.onClick.AddListener(CloseClick);
        }

        private void UpdateStats()
        {
            healthValueText.text = characterStatusController.hp.ToString();
            staminaValueText.text = characterStatusController.stamina.ToString();
            manaValueText.text = "0";
            defenceValueText.text = "0";
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
