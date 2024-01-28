using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopWalker : MonoBehaviour
{
    public float hp = 2;

    public float speed = 5f;
    private bool movingRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }
        //Debug.Log(movingRight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checker")
        {
            hp--;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("BishopWall") 
        || collision.gameObject.layer == LayerMask.NameToLayer("enemy") 
        || collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            movingRight = !movingRight; 
        }
    }
}
