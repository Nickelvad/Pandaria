using UnityEngine;
using UnityEngine.UI;

namespace Pandaria.UI.LootContainers
{
    public class LootContainerWindowController : MonoBehaviour
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
