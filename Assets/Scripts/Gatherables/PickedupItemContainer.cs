
using UnityEngine;

namespace Pandaria.Gatherables
{
    public class PickedupItemContainer
    {
        public Transform itemContainer { get; set; }
        public Transform parent { get; set; }
        public Rigidbody rigidbody { get; set; }
        public Collider collider { get; set; }
        public Vector3 velocity { get; set; }

    }

}
