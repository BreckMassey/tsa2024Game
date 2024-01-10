using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBoss : BossParent
{

    public Transform player;
    public float speed;
    public int walkDir;
    private void Start()
    {
        walkDir =(int) Mathf.Sign(0 - transform.position.x);
        player = GameObject.FindGameObjectWithTag("player").transform;
    }
    void Update()
    {
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

    void walk() {
        transform.Translate(new Vector3(walkDir*Time.deltaTime*speed, 0, 0));
    
    }
}
