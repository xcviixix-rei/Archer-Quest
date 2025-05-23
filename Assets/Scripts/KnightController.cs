using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float attackCooldown = 1f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    public DetectionZone detectionZone;
    Animator animator;
    public enum WalkDirection
    {
        Right,
        Left
    }

    private WalkDirection _walkDirection;
    private Vector2 walkableDirection = Vector2.right;
    private float attackCooldownTimer;
    public bool _isInAttackRange = false;
    public float walkToStopRate = 0.015f;

    public bool isInAttackRange
    {
        get { return _isInAttackRange; }
        private set
        {
            _isInAttackRange = value;
            animator.SetBool(AnimationStrings.isInAttackRange, value);
        }
    }
    public bool canMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public WalkDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale *= new Vector2(-1, 1);
                if (value == WalkDirection.Right)
                {
                    walkableDirection = Vector2.right;
                }
                else if (value == WalkDirection.Left)
                {
                    walkableDirection = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();

        attackCooldownTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= attackCooldownTimer)
        {
            isInAttackRange = detectionZone.detectedColliders.Count > 0;
            attackCooldownTimer = Time.time + attackCooldown;
        } 
        else
        {
            if (isInAttackRange)
            {
                isInAttackRange = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (touchingDirections.isOnWall && touchingDirections.isGrounded)
        {
            FlipDirection();
        }
        if (canMove)
        {
            rb.linearVelocity = new Vector2(walkableDirection.x * walkSpeed, rb.linearVelocity.y);
        } 
        else
        {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkToStopRate), rb.linearVelocity.y);
        }
    }

    private void FlipDirection()
    {
        if (walkDirection == WalkDirection.Right)
        {
            walkDirection = WalkDirection.Left;
        }
        else if (walkDirection == WalkDirection.Left)
        {
            walkDirection = WalkDirection.Right;
        } else 
        {
            Debug.LogError("Invalid walk direction");
        }
    }
}
