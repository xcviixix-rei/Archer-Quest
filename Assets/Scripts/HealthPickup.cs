using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    AudioSource pickupSFX;
    void Awake()
    {
        pickupSFX = GetComponent<AudioSource>();
    }

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
                if (pickupSFX) AudioSource.PlayClipAtPoint(pickupSFX.clip, gameObject.transform.position, pickupSFX.volume);
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        transform.Rotate(spinRotationSpeed * Time.deltaTime);
    }
}