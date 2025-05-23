using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightController : MonoBehaviour
{
    public float walkSpeed = 3f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    public enum WalkDirection
    {
        Right,
        Left
    }

    private WalkDirection _walkDirection;
    private Vector2 walkableDirection = Vector2.right;

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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingDirections.isOnWall && touchingDirections.isGrounded)
        {
            FlipDirection();
        }
        rb.linearVelocity = new Vector2(walkableDirection.x * walkSpeed, rb.linearVelocity.y);
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
