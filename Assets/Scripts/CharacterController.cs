using System;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    // variables for ground detection
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    private bool _jumpRequested = false;
        
    private PlayerAnimator playerAnimator;

    

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }
    private void FixedUpdate()
    {
        _rigidbody.linearVelocityX = _moveVector.x * movementSpeed;

        if(IsGrounded() && _rigidbody.linearVelocityY <= 0)
        {
            playerAnimator.handleLanding();
        }

        if(_jumpRequested && IsGrounded())
        {
            _rigidbody.linearVelocityY = jumpForce;
            _jumpRequested = false;
            playerAnimator.handleJump();
        }
        else
        {
            _jumpRequested = false;
        }

        if(_rigidbody.linearVelocityY < 0)
        {
            playerAnimator.handleFall();
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    public void Move(Vector2 moveVector)
    {
        _moveVector = moveVector;
        playerAnimator.handleMotion(moveVector);
    }
    public void Jump()
    {
        _jumpRequested = true;
    }
}