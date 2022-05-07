using UnityEngine;
using UnityEngine.UI;
using Pandaria.Gatherables;

namespace Pandaria.UI.LootContainers
{
    public class LootContainerWindowController : MonoBehaviour
    {
        public Button closeButton;
        
        void Awake()
        {
            closeButton.onClick.AddListener(closeDialog);
        }
        public void Initialize(LootContainer lootContainer)
        {

        }

        void closeDialog()
        {
            gameObject.SetActive(false);
        }
    }

}
