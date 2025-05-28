using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class KnightController : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    public DetectionZone hitboxDetectionZone;
    public DetectionZone cliffDetectionZone;
    Damageable damageable;
    Animator animator;
    public enum WalkDirection
    {
        Right,
        Left
    }

    private WalkDirection _walkDirection;
    private Vector2 walkableDirection = Vector2.right;
    public bool _isInAttackRange = false;

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
    public float attackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(0, value));
        }
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
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        isInAttackRange = hitboxDetectionZone.detectedColliders.Count > 0;
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (touchingDirections.isGrounded && touchingDirections.isOnWall)
        {
            FlipDirection();
        }
        if (!damageable.lockVelocity)
        {
            if (canMove)
            {
                float xVelocity = Mathf.Clamp(rb.linearVelocity.x + walkableDirection.x * walkAcceleration * Time.fixedDeltaTime, -maxSpeed, maxSpeed);
                rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
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
        }
        else
        {
            Debug.LogError("Invalid walk direction");
        }
    }
    public void onHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
    public void onCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }
}
