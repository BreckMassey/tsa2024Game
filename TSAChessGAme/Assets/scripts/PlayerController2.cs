using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public Rigidbody2D rb;
    public float groundHeight;
    public LayerMask groundLayer;
    public Vector2 acceleration;
    public Vector2 maxSpeeds;
    public float gravity;
    public float jumpForce;
    public Transform groundRaycastPosition;
    bool pastGrounded;
    bool grounded;
    bool spacePressed;
    bool pastSpacePressed;
    Vector2 velocity;
    public Vector2 frictions;
    Vector2 shootDir;
    public GameObject bullet;
    public float shootTime;
    float shootTimer;
    public int totalJumps;
    int jumps;
    public int health;
    int facing;
    public GameObject healthIconPrefab;
    Rigidbody2D[] healthIcons;
    float invincibility;
    public SpriteRenderer playerSprite;
    public GameObject HurtParticleEffect;
    public string[] enemyTags;
    void Start()
    {
        velocity = new Vector2(0, 0);
        shootDir = new Vector2(1, 0);

        health = Mathf.Max(health, 3);
        healthIcons = new Rigidbody2D[health];

        setUpHealth();
    }


    private void Update()
    {
        invincibility -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        Shooting();
        InvincibilityFlashing();
        repositionHealth();
    }


    void InvincibilityFlashing() {
        if (invincibility > 0)
        {
            playerSprite.color = new Color(playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                (Mathf.Sin(Time.time * 60) * 0.25f + 0.5f) * (invincibility / 3));
        }
        else {
            playerSprite.color = new Color(playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                0);
        }
    }

    void FixedUpdate()
    {
        Movement();
    } 
    void setUpHealth() {

        if (health > 0)
        {
            for (int i = 0; i < healthIcons.Length; i++)
            {
                if (healthIcons[i] != null)
                {
                    Destroy(healthIcons[i].transform.gameObject);
                }
            }


                healthIcons = new Rigidbody2D[health];
            for (int i = 0; i < health; i++)
            {
                float xOffset = ((float)i / ((float)health - 1) - 0.5f) * 0.7f * (health - 1);
                GameObject currentObject = Instantiate(healthIconPrefab, new Vector3(transform.position.x + xOffset, transform.position.y + 1.35f, transform.position.z), Quaternion.identity, transform);
                healthIcons[i] = currentObject.GetComponent<Rigidbody2D>();
            }
        }
    }

    void repositionHealth() {
        for (int i = 0; i < health; i++)
        {

            float xOffset = 0;
            if (health > 1)
            {
                xOffset=((float)i / ((float)health - 1) - 0.5f) * 0.7f * (health - 1);
            }

            xOffset -= healthIcons[i].transform.localPosition.x;
            xOffset *= 0.95f*Time.deltaTime;

            healthIcons[i].transform.Translate(xOffset, 0, 0);
        }
    }

    void Movement() {
            pastGrounded = grounded;
            grounded = false;
            RaycastHit2D hit = Physics2D.BoxCast(groundRaycastPosition.position, new Vector2(transform.localScale.x * 1f, 0.1f), 0, Vector2.down, groundHeight, groundLayer);

            if (hit.collider != null)
            {
                grounded = true;
            }
           // Debug.Log(grounded);

           
            if (grounded&&!pastGrounded)
            {
                jumps = totalJumps;
                velocity.y = Mathf.Max(velocity.y,-3f);
            }
            else
            {

                velocity.y -= gravity * Time.fixedDeltaTime;
            }
            if (!grounded && pastGrounded)
            {
                jumps = Mathf.Min(jumps, totalJumps - 1);
                velocity.y = Mathf.Max(velocity.y, 0);
            }

            spacePressed = false;
            if (Input.GetKey(KeyCode.Space))
            {
                spacePressed = true;
            }

            if (spacePressed && !pastSpacePressed && jumps > 0)
            {
                jumps--;
                velocity.y += jumpForce;
            }

        if (!Input.GetKey(KeyCode.LeftShift)||!grounded)
            {
               velocity.x += Input.GetAxis("Horizontal") * Time.fixedDeltaTime * acceleration.x;
            }

        updateShootDir();


        velocity *= frictions;

            velocity.x = Mathf.Clamp(velocity.x, -maxSpeeds.x, maxSpeeds.x);
            velocity.y = Mathf.Clamp(velocity.y, -maxSpeeds.y, maxSpeeds.y);
            rb.velocity = velocity;



            pastSpacePressed = spacePressed;
        }


    void OldMovement() {
        /*grounded = false;
           RaycastHit2D hit=Physics2D.Raycast(groundRaycastPosition.position, Vector2.down, rideHeight * 2, groundLayer);
            if (hit.collider != null) {
                if (hit.distance < rideHeight * 2)
                {
                    if (!(jumped && rb.velocity.y > 0))
                    {
                        rb.AddForce((new Vector2(0, (rideHeight - hit.distance) * springConstant)));
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * springDamping);
                    }
                    if (!grounded) { 
                        jumped = false;
                    }
                    grounded = true;
                }
                else {
                }
            }
            rb.AddForce(new Vector2(0, gravity * -1));

            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * acceleration.x*Time.deltaTime, 0));

            Debug.Log(grounded);
            if (grounded && Input.GetKey(KeyCode.Space)) 
            {
                rb.AddForce(new Vector2(0, jumpForce));
                jumped = true;
            }*/
    }


    void Shooting()
    {
        if (Input.GetKey(KeyCode.J)&&shootTimer<=0) {
            shootTimer = shootTime;
            //    Instantiate(bullet, new Vector3(transform.position.x + shootDir.normalized.x, transform.position.y + shootDir.normalized.y, -1f), Quaternion.Euler(0,0,Vector2.SignedAngle(transform.right, shootDir.normalized)));
            Instantiate(bullet, new Vector3(transform.position.x,transform.position.y, -1f), Quaternion.Euler(0, 0, Vector2.SignedAngle(transform.right, shootDir.normalized)));

        }
    }

    void updateShootDir() {

        if (Input.GetAxis("Horizontal") > 0)
        {
            shootDir.x = 1;
            facing = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            shootDir.x = -1;
            facing = -1;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            shootDir.y = 1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            shootDir.y = -1;
        }
        else {
            shootDir.y = 0;
        }

        if (Mathf.Abs(shootDir.y) == 1&& Input.GetAxis("Horizontal")==0)
        {
            shootDir.x = 0;
        }
        if (Mathf.Abs(shootDir.y) == 0 && shootDir.x == 0)
        {
            shootDir.x = facing;
        }

    }

    public void takeDamage()
    { 
        if (invincibility < 0)
        {
            health--;
            if (health >= 0)
            {
                Instantiate(HurtParticleEffect, healthIcons[health].transform.position, Quaternion.identity);
                healthIcons[health].simulated = true;
                healthIcons[health].AddForce(new Vector2(Random.Range(-100, 100), 600));
                healthIcons[health].transform.GetComponent<DieAfterTime>().enabled = true;
                invincibility = 3;
            }
            else
            {

                Debug.Log("Defeat");
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool enemy = false;

        for (int i = 0; i < enemyTags.Length; i++) {
            if (enemyTags[i] == collision.collider.tag) {
                enemy = true;
            }
        }

        if (enemy){
            takeDamage();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        bool enemy = false;

        for (int i = 0; i < enemyTags.Length; i++)
        {
            if (enemyTags[i] == collision.collider.tag)
            {
                enemy = true;
            }
        }

        if (enemy)
        {
            takeDamage();
        }
    }
}
