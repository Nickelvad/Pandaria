
using UnityEngine;

namespace Pandaria.Items
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
