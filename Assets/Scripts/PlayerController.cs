using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airSpeedX = 5f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    Rigidbody2D rb;
    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isFacingRight = true;
    TouchingDirections touchingDirections;

    public bool isMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float currentSpeed
    {
        get
        {
            if (isMoving && !touchingDirections.isOnWall)
            {
                if (touchingDirections.isGrounded) return isRunning ? runSpeed : walkSpeed;
                else return airSpeedX;
            }
            else return 0f;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        setFacingDirection(moveInput);
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }
    
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.isGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }
}
