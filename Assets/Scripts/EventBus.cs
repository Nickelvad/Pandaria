
using System;
using UnityEngine;
using Pandaria.Items;
using Pandaria.Resources;

namespace Pandaria
{
    public class EventBus : Singleton<EventBus>
    {
        public event EventHandler<GameObject> GameObjectSpotted;
        public event EventHandler<Item> ItemTracked;
        public event EventHandler<Item> ItemUntracked;
        public event EventHandler<Item> ItemPickedup;
        public event EventHandler<Item> ItemDropped;
        public event EventHandler<GatherableResourceManager> GatherableResourceTracked;
        public event EventHandler<GatherableResourceManager> GatherableResourceUntracked;
        public event EventHandler<int> GatherableResourceProgress;
        public event EventHandler<Vector3> CharacterMoved;
        public event EventHandler<int> CharacterStaminaChanged;
        public event EventHandler<int> CharacterHpChanged;


        public void CallGameObjectSpotted(object sender, GameObject gameObject)
        {
            GameObjectSpotted?.Invoke(sender, gameObject);
        }
        public void CallItemTracked(object sender, Item item)
        {
            ItemTracked?.Invoke(sender, item);
        }

        public void CallItemUntracked(object sender, Item item)
        {
            ItemUntracked?.Invoke(sender, item);
        }

        public void CallItemPickedup(object sender, Item item)
        {
            ItemPickedup?.Invoke(sender, item);
        }

        public void CallItemDropped(object sender, Item item)
        {
            ItemDropped?.Invoke(sender, item);
        }
    
        public void CallGatherableResourceTracked(object sender, GatherableResourceManager gatherableResourceManager)
        {
            GatherableResourceTracked?.Invoke(sender, gatherableResourceManager);
        }

        public void CallGatherableResourceUntracked(object sender, GatherableResourceManager gatherableResourceManager)
        {
            GatherableResourceUntracked?.Invoke(sender, gatherableResourceManager);
        }
   
        public void CallGatherableResourceProgress(object sender, int progress)
        {
            GatherableResourceProgress?.Invoke(sender, progress);
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