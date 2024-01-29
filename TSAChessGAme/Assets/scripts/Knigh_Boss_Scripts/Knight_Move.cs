using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Move : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    private bool movingUp = true; // Initial direction of movement

    void Update()
    {
        // Move the object up or down based on the current direction
        float moveY = movingUp ? speed : -speed;
        transform.Translate(0, moveY * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            // Change direction when colliding with an object on the "ground" layer
            movingUp = !movingUp;
        }
    }
}
