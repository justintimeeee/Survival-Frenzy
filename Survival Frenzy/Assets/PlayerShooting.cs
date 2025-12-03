using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;      // where the bullet spawns
    public GameObject bulletPrefab;  // bullet prefab

    [Header("Settings")]
    public float fireRate = 0.2f;    // seconds between shots

    float nextTimeToFire = 0f;

    void Update()
    {
        // Left mouse button (or Ctrl) -> "Fire1"
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("PlayerShooting: FirePoint or BulletPrefab not assigned!");
            return;
        }

        // Spawn bullet at firePoint position and rotation
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
