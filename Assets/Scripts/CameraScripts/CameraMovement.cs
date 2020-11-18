using UnityEngine;

namespace CameraScripts
{
    public class CameraMovement : CameraInput
    {
        [SerializeField] private float mouseSensitivity;

        [SerializeField] private Transform playerBody;
        
        private float _xRotation = 0f;
        private float _mouseY;
        private float _mouseX;

        // Update is called once per frame
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * _mouseX);
        }

        public override void CameraRotation(float mouseX, float mouseY)
        {
            _mouseX = mouseX * mouseSensitivity * Time.deltaTime;
            _mouseY = mouseY * mouseSensitivity * Time.deltaTime;
        }
    }
}