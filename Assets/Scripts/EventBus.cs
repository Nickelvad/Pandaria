
using System;
using UnityEngine;
using Pandaria.Items;
using Pandaria.Gatherables;

namespace Pandaria
{
    public class EventBus : Singleton<EventBus>
    {
        public event EventHandler<GameObject> GameObjectSpotted;
        public event EventHandler<PickableItemContainer> ItemContainerTracked;
        public event EventHandler<PickableItemContainer> ItemContainerUntracked;
        public event EventHandler<PickableItemContainer> ItemContainerPickedup;
        public event EventHandler<PickableItemContainer> ItemContainerDropped;
        public event EventHandler<GatherableResourceController> GatherableResourceTracked;
        public event EventHandler<GatherableResourceController> GatherableResourceUntracked;
        public event EventHandler<GatherableResourceController> GatherableResourceCollected;
        public event EventHandler<int> GatherableResourceProgress;
        public event EventHandler<LootContainer> LootContainerTracked;
        public event EventHandler<LootContainer> LootContainerUntracked;
        public event EventHandler<Vector3> CharacterMoved;
        public event EventHandler<int> CharacterStaminaChanged;
        public event EventHandler<int> CharacterHpChanged;


        public void CallGameObjectSpotted(object sender, GameObject gameObject)
        {
            GameObjectSpotted?.Invoke(sender, gameObject);
        }

        public void CallItemContainerTracked(object sender, PickableItemContainer itemContainer)
        {
            ItemContainerTracked?.Invoke(sender, itemContainer);
        }

        public void CallItemContainerUntracked(object sender, PickableItemContainer itemContainer)
        {
            ItemContainerUntracked?.Invoke(sender, itemContainer);
        }

        public void CallItemContainerPickedup(object sender, PickableItemContainer itemContainer)
        {
            ItemContainerPickedup?.Invoke(sender, itemContainer);
        }

        public void CallItemContainerDropped(object sender, PickableItemContainer itemContainer)
        {
            ItemContainerDropped?.Invoke(sender, itemContainer);
        }
    
        public void CallGatherableResourceTracked(object sender, GatherableResourceController gatherableResourceController)
        {
            GatherableResourceTracked?.Invoke(sender, gatherableResourceController);
        }

        public void CallGatherableResourceUntracked(object sender, GatherableResourceController gatherableResourceController)
        {
            GatherableResourceUntracked?.Invoke(sender, gatherableResourceController);
        }

        public void CallGatherableResourceCollected(object sender, GatherableResourceController gatherableResourceController)
        {
            GatherableResourceCollected?.Invoke(sender, gatherableResourceController);
        }
   
        public void CallGatherableResourceProgress(object sender, int progress)
        {
            GatherableResourceProgress?.Invoke(sender, progress);
        }

        public void CallLootContainerTracked(object sender, LootContainer lootContainer)
        {
            LootContainerTracked?.Invoke(sender, lootContainer);
        }

        public void CallLootContainerUntracked(object sender, LootContainer lootContainer)
        {
            LootContainerUntracked?.Invoke(sender, lootContainer);
        }

        public void CallCharacterStaminaChanged(object sender, int currentStamina)
        {
            CharacterStaminaChanged?.Invoke(sender, currentStamina);
        }

        public void CallCharacterMoved(object sender, Vector3 position)
        {
            CharacterMoved?.Invoke(sender, position);
        }

        public void CallCharacterHpChanged(object sender, int damage)
        {
            CharacterHpChanged?.Invoke(sender, damage);
        }
    }
}