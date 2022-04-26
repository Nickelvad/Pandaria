using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.Islands
{
    public class FloatingIsland : MonoBehaviour
    {
        Vector3 zeroPoint;
        public float speed = 10f;
        private Rigidbody rigidbody_;
        private Vector3 direction;

        public void Initialize(Vector3 zeroPoint)
        {
            this.zeroPoint = zeroPoint;
        }

        void Start()
        {
            rigidbody_ = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 islandHeighPoint = new Vector3(zeroPoint.x, transform.position.y, zeroPoint.x);
            direction = (islandHeighPoint - transform.position).normalized;
            rigidbody_.MovePosition(rigidbody_.position + direction * Time.deltaTime * speed);
        }
    }
}
