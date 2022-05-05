using UnityEngine;
using UnityEngine.UI;

namespace Pandaria.UI
{
    public class ControlBarController : MonoBehaviour
    {
        public Button inventoryButton;
        public Button characterButton;
        public GameObject inventoryWindow;
        public GameObject characterWindow;

        void Awake()
        {
            inventoryButton.onClick.AddListener(ShowInventoryWindow);
            characterButton.onClick.AddListener(ShowCharacterWindow);
        }

        void ShowInventoryWindow()
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }

        void ShowCharacterWindow()
        {
            characterWindow.SetActive(!characterWindow.activeSelf);
        }
    }
}

