using UnityEngine;

namespace CameraScripts
{
    public abstract class CameraInput : MonoBehaviour
    {
        public abstract void CameraRotation(float mouseX, float mouseY);
    }
}