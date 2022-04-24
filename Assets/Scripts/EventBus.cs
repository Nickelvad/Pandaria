
using System;
using UnityEngine;
using Pandaria.Items;

namespace Pandaria
{
    public class EventBus : Singleton<EventBus>
    {
        public event EventHandler<GameObject> GameObjectSpotted;
        public event EventHandler<Item> ItemTracked;
        public event EventHandler<Item> ItemUntracked;
        public event EventHandler<Item> ItemPickedup;
        public event EventHandler<Item> ItemDropped;
        public event EventHandler<GatherableResource> GatherableResourceTracked;
        public event EventHandler<GatherableResource> GatherableResourceUntracked;
        public event EventHandler<int> GatherableResourceProgress;


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
            Debug.Log("Called event item dropped bus");
            ItemDropped?.Invoke(sender, item);
        }
    
        public void CallGatherableResourceTracked(object sender, GatherableResource gatherableResource)
        {
            GatherableResourceTracked?.Invoke(sender, gatherableResource);
        }

        public void CallGatherableResourceUntracked(object sender, GatherableResource gatherableResource)
        {
            GatherableResourceUntracked?.Invoke(sender, gatherableResource);
        }
   
        public void CallGatherableResourceProgress(object sender, int progress)
        {
            GatherableResourceProgress?.Invoke(sender, progress);
        }
    }
}