using UnityEngine;
using UnityEngine.UI;


namespace Pandaria.UI
{
    public class InventoryWindowController : MonoBehaviour
    {
        public Button closeButton;

        void Awake()
        {
            closeButton.onClick.AddListener(closeDialog);
        }

        void closeDialog()
        {
            gameObject.SetActive(false);
        }
    }
}

