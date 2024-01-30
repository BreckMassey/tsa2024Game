using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Sweeper_Knight : MonoBehaviour
{
    public float speed = 10f;
    private bool movingUp = true;  
    private bool startMovingLeft = false;  

    void Update()
    {
        if (movingUp)
        {
            
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (startMovingLeft)
        {
            
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (movingUp&&transform.position.y>=-7.5f)
        {
            movingUp = false;
            startMovingLeft = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BishopWalker"))
        {
            if (movingUp)
            {
                movingUp = false;
                startMovingLeft = true;
            }
        }

        if (collision.gameObject.CompareTag("player"))
         {
            collision.collider.GetComponent<PlayerController2>().takeDamage();

            Destroy(gameObject);
         }
    }

}
