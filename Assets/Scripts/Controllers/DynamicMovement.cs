using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DynamicMovement : Movement
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private bool flipped;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float maxGroundDistance = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float jumpSpeed = 6;
    [SerializeField] private Animator animator;    
    
    private float _horizontal;
    private float _vertical;
    private bool _grounded;
    private bool _sit;
    private RaycastHit[] groundHits = new RaycastHit[1];
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _jumping;
    private int _jump;
    private static readonly int SpeedAnimator = Animator.StringToHash("Speed");
    private static readonly int SitAnimator = Animator.StringToHash("Sit");
    private static readonly int JumpTriggerAnimator = Animator.StringToHash("Jump");
    private static readonly int GroundedAnimator = Animator.StringToHash("Grounded");


    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Sit()
    {
        if (!_sit)
        {
            _capsuleCollider.height = 0.8f;
            horizontalSpeed = horizontalSpeed / 2;
            animator.SetBool(SitAnimator, true);
            _sit = true;
        }
        else
        {
            horizontalSpeed = horizontalSpeed * 2;
            animator.SetBool(SitAnimator, false);
            _capsuleCollider.height = 1.15f;
            _sit = false;
        }
    }

    public override void Move(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        animator.SetFloat(SpeedAnimator, Mathf.Abs(_horizontal));
        _vertical = vertical;
        if (horizontal > 0 && flipped || horizontal < 0 && !flipped)
        {
            Flip();
        }
    }

    public override void Jump()
    {
        _jump = CanJump() ? 1 : 0;
    }

    private bool CanJump()
    {
        if (!_jumping && _grounded)
        {
            animator.SetTrigger(JumpTriggerAnimator);
            _jumping = true;
            return true;
        }
        return false;
    }

    private void CheckGround()
    {
        var wasGrounded = _grounded;
        _grounded = false;
        foreach (var check in groundChecks)
        {
            if (Physics.RaycastNonAlloc(check.position, Vector2.down, groundHits, maxGroundDistance, groundLayers) >
                0)
            {
                if (!_grounded)
                    _jumping = false;
                _grounded = true;
                break;
            }
        }
        
        if (wasGrounded != _grounded)
        {
            animator.SetBool(GroundedAnimator, _grounded);
        }
            
    }

    private void Flip(bool updateFlip = true)
    {
        flipped = updateFlip ? !flipped : flipped;
        var flipValue = flipped ? -1 : 1;
        _transform.localScale = new Vector3(flipValue,1,1);
    }

    private void FixedUpdate()
    {
        //Check if grounded
        CheckGround();
        
        //Moving Forwakd/Backward
        var velocity = _rigidbody.velocity;
        velocity.x = _horizontal * horizontalSpeed;
        
        //Juming
        if (_jump > 0)
        {
            velocity.y = jumpSpeed;
            _jump = 0;
        }
        
        //Applying
        _rigidbody.velocity = velocity;
    }

    private void OnValidate()
    {
        _transform = transform;
        Flip(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (var check in groundChecks)
        {
            var position = check.position;
            Gizmos.DrawLine(position, position + Vector3.down * maxGroundDistance);
        }
    }
}