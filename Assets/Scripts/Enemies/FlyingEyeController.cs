using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public float fligtSpeed = 2f;
    public DetectionZone hitboxDetectionZone;
    Damageable damageable;
    Animator animator;
    Rigidbody2D rb;
    public List<Transform> wayPoints = new List<Transform>();
    int wayPointNumber = 0;
    public Transform nextWayPoint;
    public float wayPointReachedDistance = 0.01f;
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

    void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextWayPoint = wayPoints[wayPointNumber];
    }
    void FixedUpdate()
    {
        if (damageable.isAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isInAttackRange = hitboxDetectionZone.detectedColliders.Count > 0;
    }
    public void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(directionToWayPoint, transform.position);
        rb.linearVelocity = directionToWayPoint * fligtSpeed;
        if (distance <= wayPointReachedDistance)
        {
            wayPointNumber++;
            if (wayPointNumber >= wayPoints.Count) wayPointNumber = 0;
            nextWayPoint = wayPoints[wayPointNumber];
        }
    }
    public void onHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}
