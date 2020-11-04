using UnityEngine;

namespace PickableObjects
{
    public interface IPickable
    {
        void TakeObject(GameObject obj);
    }
}