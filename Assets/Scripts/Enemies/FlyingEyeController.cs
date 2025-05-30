using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public float fligtSpeed = 2f;
    public DetectionZone hitboxDetectionZone;
    Damageable damageable;
    Animator animator;
    Rigidbody2D rb;
    public  Collider2D deathCollider;
    public List<Transform> wayPoints = new List<Transform>();
    int wayPointNumber = 0;
    public Transform nextWayPoint;
    public float wayPointReachedDistance = 0.1f;
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
    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(onDeath);
    }
    public void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        Debug.Log(distance);
        rb.linearVelocity = directionToWayPoint * fligtSpeed;
        FlipDirection();
        if (distance <= wayPointReachedDistance)
        {
            wayPointNumber++;
            if (wayPointNumber >= wayPoints.Count) wayPointNumber = 0;
            nextWayPoint = wayPoints[wayPointNumber];
            Debug.Log("This line was ran");
        }
    }
    private void FlipDirection()
    {
        if (transform.localScale.x > 0)
        {
            if (rb.linearVelocityX < 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x,
                                                    transform.localScale.y,
                                                    transform.localScale.x);
            }
        }
        else if (transform.localScale.x < 0)
        {
            if (rb.linearVelocityX > 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x,
                                                    transform.localScale.y,
                                                    transform.localScale.x);
            }
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
    public void onDeath()
    {
        rb.gravityScale = 0.7f;
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        deathCollider.enabled = true;
    }
}
