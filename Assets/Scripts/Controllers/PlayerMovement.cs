using UnityEngine;

namespace Controllers
{
    public class PlayerMovement : Movement
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
        
        private float _x;
        private float _z;
        private bool _jump;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            Vector3 move = _transform.right * _x + _transform.forward * _z;
            controller.Move(move);

            if (_jump && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                _jump = false;
            }

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }

        public override void Move(float x, float z)
        {
            _x = x * speed * Time.deltaTime;
            _z = z * speed * Time.deltaTime;
        }

        public override void Jump()
        {
            _jump = true;
        }

        public override void Run(bool state)
        {
            if (state)
                speed *= 2;
            else
                speed /= 2;
        }
    }
}
