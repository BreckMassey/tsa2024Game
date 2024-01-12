using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player") {
            collision.GetComponent<PlayerController2>().takeDamage();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            collision.GetComponent<PlayerController2>().takeDamage();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "player")
        {
            collision.collider.GetComponent<PlayerController2>().takeDamage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "player")
        {
            collision.collider.GetComponent<PlayerController2>().takeDamage();
        }

    }
}
