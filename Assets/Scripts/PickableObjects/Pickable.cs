using UnityEngine;

namespace PickableObjects
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private GameObject obj;

        private void OnTriggerEnter(Collider other)
        {
            other.attachedRigidbody.GetComponent<IPickable>()?.TakeObject(obj);
        }
    
        private void OnCollisionEnter(Collision other)
        {
            other.rigidbody.GetComponent<IPickable>()?.TakeObject(obj);
        }

    }
}