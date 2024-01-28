using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the inspector
    public float shootingInterval = 2f; // Time between shots
    public float spawnOffset = 1f;
    private float timer;

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if it's time to shoot
        if (timer >= shootingInterval)
        {
            ShootBullet();
            timer = 0f; // Reset the timer
        }
    }

    void ShootBullet()
    {
        // Instantiate the bullet at knight's position and rotation
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
