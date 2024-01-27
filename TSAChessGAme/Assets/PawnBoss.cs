using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBoss : BossParent
{

    public Transform player;
    public float speed;
    public int walkDir;
    float timeAlive;
    float intY;
    public bool canJump;
    private void Start()
    {
        walkDir =(int) Mathf.Sign(0 - transform.position.x);
        player = GameObject.FindGameObjectWithTag("player").transform;

        intY = transform.position.y;
    }
    void Update()
    {

        timeAlive += Time.deltaTime;
        //followPlayer();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        walk();
    }
    void followPlayer() {
        Vector2 dir = player.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * Time.deltaTime * speed) ;

        
    }

    public void setJump(bool a) {
        canJump = a;
    }

    void walk() {
        transform.Translate(new Vector3(walkDir*Time.deltaTime*speed, 0, 0));
        if (timeAlive > 2.0f&& canJump)
        {
           // Debug.Log("hello");
            transform.position = new Vector2(transform.position.x, intY + Mathf.Abs(Mathf.Sin((timeAlive-2) * 6) * 4));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "checker")
        {
            health--;
        }
    }
}
