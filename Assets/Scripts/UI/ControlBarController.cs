using UnityEngine;
using UnityEngine.UI;

namespace Pandaria.UI
{
    public class ControlBarController : MonoBehaviour
    {
        public Button inventoryButton;
        public GameObject inventoryWindow;

        void Awake()
        {
            inventoryButton.onClick.AddListener(ShowInventoryWindow);
        }

        void ShowInventoryWindow()
        {
            inventoryWindow.SetActive(true);
        }
    }
}

