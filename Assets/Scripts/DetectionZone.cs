using UnityEngine;
using System.Collections.Generic;

public class DetectionZone : MonoBehaviour
{
    BoxCollider2D detectionZone;
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    void Awake()
    {
        detectionZone = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != detectionZone)
        {
            detectedColliders.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other != detectionZone)
        {
            detectedColliders.Remove(other);
        }
    }
}
