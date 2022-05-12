using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurretAmmo : MonoBehaviour
{
    public LayerMask layersToIgnore;
    public GameObject centerOfMass;
    private Rigidbody _rigidbody;
    private Collider _collider;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _rigidbody.centerOfMass = centerOfMass.transform.position;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && (((1 << other.gameObject.layer) & layersToIgnore) != 0))
        {
            return;
        }
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.SetParent(other.gameObject.transform);
        Invoke(nameof(DestroyAmmo), 3f);
    }

    void DestroyAmmo()
    {
        Destroy(this.gameObject);
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetPosition(float firingPowerAmmunitionOffset)
    {
        transform.position -= transform.forward * Time.deltaTime * firingPowerAmmunitionOffset;
    }

    public void Fire(float firingPower)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * firingPower, ForceMode.Impulse);
        _collider.enabled = true;
        transform.SetParent(null);
    }
}
