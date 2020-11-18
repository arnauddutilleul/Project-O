using UnityEngine;

namespace Controllers
{
    public abstract class Movement : MonoBehaviour
    {
        public abstract void Move(float x, float z);
        public abstract void Jump();
        public abstract void Run(bool state);
    }
}