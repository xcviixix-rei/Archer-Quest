using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 speed = new Vector2(4f, 0f);
    public Vector2 knockback = new Vector2(0f, 0f);
    public int damage = 5;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(speed.x * transform.localScale.x, speed.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        if (damageable != null)
        {
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log("Attack hit: " + collision.name + " with damage: " + damage);
                Destroy(gameObject);
            }
        }
    }
}
