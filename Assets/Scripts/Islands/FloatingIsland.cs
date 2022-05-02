using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pandaria.Islands
{
    public class FloatingIsland : MonoBehaviour
    {
        Vector3 destination;
        public float speed = 10f;
        public Transform leftPoint;
        public Transform rightPoint;
        private Rigidbody rigidbody_;
        private Vector3 direction;
        private RaycastHit hit;
        private bool[] hitResults;
        private int centerIndex;
        private List<Transform> points;
        private Ray ray;

        public void Initialize(Vector3 destination)
        {
            rigidbody_ = GetComponent<Rigidbody>();
            points = new List<Transform>{leftPoint, this.transform, rightPoint};
            hitResults = new bool[points.Count];
            centerIndex = (points.Count - 1) / 2;
            this.destination = destination;
            Vector3 lookPosition = destination - transform.position;
            lookPosition.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPosition);
            transform.rotation = rotation;
            rigidbody_.rotation = rotation;
            direction = (destination - transform.position).normalized;
        }

        void FixedUpdate()
        {

            if (Mathf.Abs(transform.position.x) > 200f || Mathf.Abs(transform.position.y) > 200f)
            {
                Destroy(this.gameObject);
                return;
            }

            CheckIslandsInFront();
            int rotationDirection = GetRotateDirection();

            if (rotationDirection != 0)
            {
                transform.RotateAround(transform.position, Vector3.up, Random.Range(rotationDirection, rotationDirection * 2));
            }

            rigidbody_.MovePosition(rigidbody_.position + transform.forward * Time.deltaTime * speed);
            transform.position = rigidbody_.position;
        }

        private bool CheckDirection(Vector3 position)
        {
            ray = new Ray(position, transform.forward);
            return Physics.Raycast(ray, out hit, 30f);
        }

        private int GetRotateDirection()
        {
            int left = 0;
            int right = 0;
            for (int i = 0; i <= hitResults.Length - 1; i++)
            {
                if (!hitResults[i])
                {
                    continue;
                }

                if (i < centerIndex)
                {
                    left += 1;
                }

                if (i > centerIndex)
                {
                    right += 1;
                }

                if (i == centerIndex)
                {
                    left += 1;
                    right += 1;
                }
                
            }

            if (left == 0 && right == 0)
            {
                return 0;
            }

            if (left > right)
            {
                return 1;
            }

            if (left < right)
            {
                return -1;
            }

            return 1;
        }

        void CheckIslandsInFront()
        {
            for (int i = 0; i <= points.Count - 1; i++)
            {
                hitResults[i] = CheckDirection(points[i].position);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 30);
        }
    }
}
