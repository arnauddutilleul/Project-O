﻿using UnityEngine;

namespace CameraController
{
    public class CameraLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;

        [SerializeField] private Transform playerBody;
        
        private float _xRotation = 0f;

        // Update is called once per frame
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}