using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Bullet_Behavior : MonoBehaviour
{
    public float speed = 5f;
    private GameObject player;
    private bool hasRotated = false;

    void Start()
    {
        player = GameObject.FindWithTag("player"); // Assuming the player has the tag "Player"
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (!hasRotated && Mathf.Abs(transform.position.x - player.transform.position.x) < 0.1f)
        {
            RotateTowardsPlayer();
            hasRotated = true;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angle *= -1;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        speed *= 2;
    }

    // Uncomment the method that matches your collider setup:
    
     
     /*void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("player"))
        {
             Destroy(gameObject);
         }
     }*/

    // Regular Collider
     void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("player"))
         {
             Destroy(gameObject);
         }
     }
}
