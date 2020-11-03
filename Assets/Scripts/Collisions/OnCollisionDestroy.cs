using System;
using UnityEngine;
using Pooling;
using UnityEngine.Events;

public class OnCollisionDestroy : MonoBehaviour
{
    [SerializeField] private AffectedObject affectedObject;
    [SerializeField] private bool destroyOnCollision;
    [SerializeField] private bool destroyOnTrigger;
    [SerializeField] private UnityEvent onDestroyed;
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (destroyOnCollision)
            DestroyObjects(other.rigidbody.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnTrigger)
            DestroyObjects(other.attachedRigidbody.gameObject);
    }

    private void DestroyObjects(GameObject other)
    {
        onDestroyed?.Invoke();
        switch (affectedObject)
        {
            case AffectedObject.Self:
                gameObject.TryRelease();
                break;
            case AffectedObject.Other:
                other.TryRelease();
                break;
            case AffectedObject.Both:
                gameObject.TryRelease();
                other.TryRelease();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public enum AffectedObject
    {
        Self,
        Other,
        Both,
    }

}

