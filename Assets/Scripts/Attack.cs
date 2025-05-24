using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage1 = 10;
    public Vector2 knockback = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        Vector2 deliveredKnockback = transform.parent.transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        if (damageable != null)
        {
            bool gotHit = damageable.Hit(attackDamage1, deliveredKnockback);
            if (gotHit) Debug.Log("Attack hit: " + other.name + " with damage: " + attackDamage1);
        }
    }
}
