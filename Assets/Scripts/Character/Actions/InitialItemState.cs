
using UnityEngine;

namespace Pandaria.Character.Actions
{
    public class InitialItemState
    {
        public Transform item { get; set; }
        public Transform parent { get; set; }
        public Rigidbody rigidbody { get; set; }
        public Collider collider { get; set; }
        public Vector3 velocity { get; set; }

    }

}
