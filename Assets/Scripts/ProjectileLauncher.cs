using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 OriginalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(OriginalScale.x * transform.localScale.x > 0 ? 1 : -1, OriginalScale.y, OriginalScale.z);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
