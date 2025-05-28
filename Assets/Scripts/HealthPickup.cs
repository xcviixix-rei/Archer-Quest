using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null) // Check if the colliding object has a Damageable component
        {
            // Try to heal the damageable object.
            // The Heal method will return true if healing was applied, false otherwise.
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                // If healing was successful (e.g., player wasn't at full health),
                // destroy the pickup object.
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        transform.Rotate(spinRotationSpeed * Time.deltaTime);
    }
}