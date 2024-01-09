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
    void Start()
    {
        velocity = new Vector2(0, 0);
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        Shooting();
    }


    void FixedUpdate()
    {
        Movement();
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
            shootDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Instantiate(bullet, new Vector3(transform.position.x + shootDir.normalized.x, transform.position.y + shootDir.normalized.y, -1f), Quaternion.Euler(0,0,Vector2.SignedAngle(transform.right, shootDir.normalized)));
        }
    }
}
