using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        private Vector3 _velocity;
        private bool _isGrounded;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed *= 2;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                speed /= 2;

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            Vector3 move = _transform.right * x + _transform.forward * z;
            controller.Move(move);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }
    }
}
