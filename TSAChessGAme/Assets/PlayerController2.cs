using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public Rigidbody2D rb;
    public float rideHeight;
    public float springConstant;
    public float springDamping;
    public LayerMask groundLayer;
    public Vector2 acceleration;
    public Vector2 maxSpeeds;
    public float gravity;
    public float jumpForce;
    public Transform groundRaycastPosition;
    bool grounded;
    bool jumped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       grounded = false;
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
        }
    }
}
