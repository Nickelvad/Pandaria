using UnityEngine;
using UnityEngine.UI;
using Pandaria.Gatherables;
using Pandaria.Characters.Actions;

namespace Pandaria.UI.LootContainers
{
    public class LootContainerWindowController : MonoBehaviour
    {
        public Button closeButton;
        public Button takeAllButton;
        public GameObject lootSlotPrefab;
        public GameObject lootSlotsParent;
        public CharacterLootController characterLootController;
        private LootContainer lootContainer;
        
        void Awake()
        {
            closeButton.onClick.AddListener(closeDialog);
            takeAllButton.onClick.AddListener(TakeAll);
        }
        public void Initialize(LootContainer lootContainer)
        {
            this.lootContainer = lootContainer;
            InitializeSlots();

        }

        private void CleanSlots(GameObject gameObjectForCleanup)
        {
            while (gameObjectForCleanup.transform.childCount > 0)
            {
                DestroyImmediate(gameObjectForCleanup.transform.GetChild(0).gameObject);
            }
        }

        void InitializeSlots()
        {
            foreach (var item in lootContainer.lootContent)
            {
                var slot = Instantiate(lootSlotPrefab, lootSlotsParent.transform.position, Quaternion.identity, lootSlotsParent.transform);
                LootContainerSlotController controller = slot.GetComponent<LootContainerSlotController>();
                controller.Initialize(item);
            }
        }

        void Refresh()
        {
            CleanSlots(lootSlotsParent);
            InitializeSlots();
        }

        void OnDisable()
        {
            CleanSlots(lootSlotsParent);
        }

        void closeDialog()
        {
            gameObject.SetActive(false);
        }

        private void TakeAll()
        {
            characterLootController.TakeAll();
            Refresh();
            if (lootContainer.lootContent.Count == 0)
            {
                closeDialog();
            }
        }
    }

}
