using UnityEngine;
using Pandaria.Inputs;

namespace Pandaria.Buildings
{
    public class TurretController : MonoBehaviour
    {
        public float verticalRotationMin = 340f;
        public float verticalRotationMax = 45f;
        public GameObject ammunitionPrefab;
        public GameObject ammunitionSpawnPosition;
        private Vector3 rotationDirection;
        private Vector3 newRotation;
        private float vertical = 0f;
        private float horizontal = 0f;
        private bool preparedToFire = false;
        private GameObject loadedAmmunition;
        private Rigidbody loadedAmmunitionRigidbody;
        
        void Update()
        {
            Rotation();
            PrepareToFire();
            RotateAmmunition();
            Fire();
        }

        private void RotateAmmunition()
        {
            if (loadedAmmunition != null)
            {
                loadedAmmunition.transform.rotation = transform.rotation;
            }
            
        }

        private void PlaceAmmunition()
        {
            if (loadedAmmunition != null)
            {
                loadedAmmunition.transform.position = ammunitionSpawnPosition.transform.position;
            }
        }


        public void PrepareToFire()
        {
            if (preparedToFire)
            {
                return;
            }

            loadedAmmunition = Instantiate(ammunitionPrefab, ammunitionSpawnPosition.transform.position, Quaternion.identity);
            loadedAmmunitionRigidbody = loadedAmmunition.GetComponent<Rigidbody>();
            loadedAmmunitionRigidbody.isKinematic = true;
            preparedToFire = true;
        }

        private void Rotation()
        {
            rotationDirection = new Vector3(InputController.Instance.GetVertical(), InputController.Instance.GetHorizontal(), 0f);
            
            vertical += rotationDirection.x;
            horizontal += rotationDirection.y;
            if (horizontal < 0)
            {
                horizontal += 360f;
            }
            if (horizontal > 360f)
            {
                horizontal -= 360f;
            }

            if (vertical > verticalRotationMax)
            {
                vertical = verticalRotationMax;
            }
            if (vertical < verticalRotationMin)
            {
                vertical = verticalRotationMin;
            }
            newRotation = new Vector3(180f + vertical, horizontal, 180f);
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                loadedAmmunitionRigidbody.isKinematic = false;
                loadedAmmunitionRigidbody.AddForce(loadedAmmunition.transform.forward * 10f, ForceMode.Impulse);
                loadedAmmunition = null;
                preparedToFire = false;
            }
        }

        void FixedUpdate()
        {
            transform.localEulerAngles = newRotation;
            PlaceAmmunition();
        }
    }

}
