using HealthManagement;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int value;

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.GetComponent<IDamageable>()?.TakeDamage(value);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        other.rigidbody.GetComponent<IDamageable>()?.TakeDamage(value);
    }

}