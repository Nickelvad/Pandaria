using UnityEngine;
using UnityEngine.UI;
using Pandaria.Gatherables;

namespace Pandaria.UI.LootContainers
{
    public class LootContainerWindowController : MonoBehaviour
    {
        public Button closeButton;
        public GameObject lootSlotPrefab;
        public GameObject lootSlotsParent;
        private LootContainer LootContainer;
        
        void Awake()
        {
            closeButton.onClick.AddListener(closeDialog);
        }
        public void Initialize(LootContainer lootContainer)
        {
            this.LootContainer = lootContainer;
            foreach (var item in lootContainer.lootContent)
            {
                var slot = Instantiate(lootSlotPrefab, lootSlotsParent.transform.position, Quaternion.identity, lootSlotsParent.transform);
                LootContainerSlotController controller = slot.GetComponent<LootContainerSlotController>();
                controller.Initialize(item);
            }
        }

        void closeDialog()
        {
            gameObject.SetActive(false);
        }
    }

}
