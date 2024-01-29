using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float shootingInterval = 2f; 
    public float spawnOffset = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootingInterval)
        {
            ShootBullet();
            timer = 0f; // Reset the timer
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
