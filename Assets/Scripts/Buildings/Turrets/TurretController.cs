using UnityEngine;
using Pandaria.Inputs;

namespace Pandaria.Buildings.Turrets
{
    public class TurretController : MonoBehaviour
    {
        public float verticalRotationMin = 340f;
        public float verticalRotationMax = 45f;
        public GameObject ammoPrefab;
        public GameObject ammoSpawnPosition;
        public float initialFiringPower = 10f;
        public float maxFiringPower = 40f;
        public float firingPowerIncreasePerSecond = 10f;
        public float firingPowerAmmoOffset = 0.1f;

        private Vector3 rotationDirection;
        private Vector3 newRotation;
        private float vertical = 0f;
        private float horizontal = 0f;
        private bool preparedToFire = false;
        private bool preparingToFire = false;
        private bool firing = false;
        public float firingPower = 10f;
        private GameObject loadedAmmo;
        private BaseTurretAmmo baseTurretAmmo;
        
        void Update()
        {
            CalculateRotation();
            PrepareToFire();
            RotateAmmo();
            Fire();
        }

        void FixedUpdate()
        {
            Rotate();
            MoveAmmo();
        }

        private void RotateAmmo()
        {
            if (loadedAmmo != null)
            {
                baseTurretAmmo.SetRotation(transform.rotation);
            }
            
        }

        private void MoveAmmo()
        {
            if (firing & firingPower < maxFiringPower)
            {
                baseTurretAmmo.SetPosition(firingPowerAmmoOffset);
            }
        }

        public void PrepareToFire()
        {
            if (preparedToFire || preparingToFire)
            {
                return;
            }

            Invoke(nameof(Prepare), 2);
            preparingToFire = true;
        }

        private void Prepare()
        {
            loadedAmmo = Instantiate(ammoPrefab, ammoSpawnPosition.transform.position, Quaternion.identity, this.transform);
            baseTurretAmmo = loadedAmmo.GetComponent<BaseTurretAmmo>();
            preparedToFire = true;
            preparingToFire = false;
        }

        private void CalculateRotation()
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

        private void Rotate()
        {
            transform.localEulerAngles = newRotation;
        }

        private void Fire()
        {
            if (preparingToFire) { return; }
            if (!preparedToFire) { return; }
    
            if (Input.GetButtonUp("Fire1") && firing)
            {
                baseTurretAmmo.Fire(firingPower);
                loadedAmmo = null;
                preparedToFire = false;
                firing = false;
                firingPower = initialFiringPower;
            }

            if (Input.GetButtonDown("Fire1") && !firing)
            {
                firing = true;
            }

            if (firing && firingPower < maxFiringPower)
            {
                firingPower += firingPowerIncreasePerSecond * Time.deltaTime;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (loadedAmmo != null)
            {
                Gizmos.color = Color.blue;
                Vector3 sight = transform.position + transform.forward * 5;

                Gizmos.DrawLine(loadedAmmo.transform.position, sight);
            }

        }
    }

}
