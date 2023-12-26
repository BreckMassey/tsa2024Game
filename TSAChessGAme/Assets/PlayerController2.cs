using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public Rigidbody2D rb;
    public Vector2 rideHeight;
    public Vector2 springConstant;
    public Vector2 springDamping;
    public LayerMask groundLayer;
    public Vector2 acceleration;
    public float gravity;
    public float jumpForce;
    public Transform groundRaycastPosition;
    bool grounded;
    bool jumped;
    public float frictionConstant;
    public Vector2 maxVelocity;
    bool hitWall;
    public Vector2 airDrag;
    public bool spaceHitLastFrame;
    public bool crouching;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        hitWall = false;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity.x, maxVelocity.x), Mathf.Clamp(rb.velocity.y, -maxVelocity.y, maxVelocity.y));

        grounded = false;
       RaycastHit2D hit=Physics2D.Raycast(groundRaycastPosition.position, Vector2.down, rideHeight.y * 2, groundLayer);
        if (hit.collider != null) {
            if (hit.distance < rideHeight.y * 2)
            {
                if (!(jumped && rb.velocity.y > 0))
                {
                //    rb.velocity+=((new Vector2(0, (rideHeight.y - hit.distance) * springConstant.y)));
                  //  rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * springDamping.y);
                }
                if (rb.velocity.y<0) { 
                    jumped = false;
                }
                grounded = true;
            }
            else {
            }
        }
        
        
        /*RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector2.right, rideHeight.x * 2, groundLayer);
        if (hitR.collider != null)
        {
            Debug.Log(hitR.distance+" Right");
            if (hitR.distance < rideHeight.x * 2)
            {
                hitWall = true;
                Debug.Log(hitR.distance);
                Debug.Log((rideHeight.x - hitR.distance) * springConstant.x);
                rb.velocity = new Vector2(Mathf.Max(rideHeight.x - hitR.distance,0) * -springConstant.x, rb.velocity.y);
                    rb.velocity = new Vector2(rb.velocity.x * springDamping.x, rb.velocity.y);
            }
        }

        RaycastHit2D hitL = Physics2D.Raycast(transform.position, Vector2.left, rideHeight.x * 2, groundLayer);
        if (hitL.collider != null)
        {
            Debug.Log(hitL.distance+" Left");
            if (hitL.distance < rideHeight.x * 2)
            {
                hitWall = true;
                Debug.Log(hitL.distance);
                rb.velocity += ((new Vector2((rideHeight.x - hitL.distance) * springConstant.x, 0)));
                rb.velocity = new Vector2(rb.velocity.x * springDamping.x, rb.velocity.y);
            }
        }
        */

        rb.velocity+=(new Vector2(0, gravity * -1*Time.fixedDeltaTime));

        rb.velocity += (new Vector2(Input.GetAxis("Horizontal") * acceleration.x*Time.fixedDeltaTime, 0));

        crouching = Input.GetKey(KeyCode.S); 
        
        


        if (grounded && Input.GetKey(KeyCode.Space)&&!spaceHitLastFrame&&!crouching) 
        {
            rb.velocity += (new Vector2(0, jumpForce));
            jumped = true;
        }

        if (grounded&&!(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))&&hitWall == !true)
        {
          //  rb.velocity = new Vector2(rb.velocity.x  * frictionConstant * Time.deltaTime, rb.velocity.y);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (!grounded) {
            rb.velocity = new Vector2(rb.velocity.x * airDrag.x, rb.velocity.y * airDrag.y);
        }


        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity.x, maxVelocity.x), Mathf.Clamp(rb.velocity.y, -maxVelocity.y, maxVelocity.y));


        spaceHitLastFrame = Input.GetKey(KeyCode.Space);
    }
}
