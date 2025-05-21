using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform trackingTarget;
    float startingZ;
    Vector2 startingPos;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - (Vector2)startingPos;
    float zDistanceFromTarget => transform.position.z - trackingTarget.transform.position.z;
    float clippingPlane => cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);
    float paralaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startingPos + camMoveSinceStart * paralaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }

    void FixedUpdate()
    {
        
    }
}
