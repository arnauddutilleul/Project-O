using Pooling;
using UnityEngine;

namespace Pooling
{
    public class Pooled : MonoBehaviour
    {
        public ObjectPool pool;

        public bool TryRelease()
        {
            return pool.TryRelease(gameObject);
        }
    }
}