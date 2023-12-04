using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 boxColliderSize;
    public Vector2 velocity=new Vector2(0,0);
    public LayerMask playerCollisionLayers;
    float skinWidth = 0.015f;
    public float speed;
    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        boxColliderSize = transform.localScale;
        boxColliderSize.x -= skinWidth;
        boxColliderSize.y -= skinWidth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.x = Input.GetAxis("Horizontal") * speed*Time.fixedDeltaTime;
        velocity.y += Input.GetAxis("Vertical") * speed*Time.fixedDeltaTime;
        velocity.y -= gravity*Time.fixedDeltaTime;

        Debug.Log("before "+velocity);
        Debug.Log("call");
        velocity = collideAndSlide(transform.position, velocity, 5,velocity);
        Debug.Log("AFter "+velocity);
        transform.Translate(velocity*Time.fixedDeltaTime);
    }

    public Vector2 collideAndSlide(Vector2 pos, Vector2 vel, int depth,Vector2 velocityInit) {
        if (depth <= 0) {
            return Vector2.zero;
        }
       
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxColliderSize, /*transform.eulerAngles.z*/0, velocity.normalized, velocity.magnitude*Time.fixedDeltaTime + skinWidth, playerCollisionLayers);


        if (hit.collider != null)
        {
            Debug.Log("Hit Normal" + hit.normal);
            Vector2 snapToSurface = vel.normalized * (hit.distance - skinWidth);//new Vector2(velocity.x * Time.fixedDeltaTime - castOutput.distance * velocity.normalized.x, velocity.y * Time.fixedDeltaTime - castOutput.distance * velocity.normalized.y);
            Debug.Log("snap "+snapToSurface);
            Vector2 leftOver = vel - snapToSurface;
            float angle = Vector2.Angle(Vector2.up, hit.normal);
            if (snapToSurface.magnitude <= skinWidth) {
                snapToSurface = Vector2.zero;
            }
            float mag = leftOver.magnitude;
            leftOver = Vector3.ProjectOnPlane(new Vector3(leftOver.x,leftOver.y,0),new Vector3(hit.normal.x,hit.normal.y,0)).normalized;
            Debug.Log("projected " + leftOver);
            leftOver *= mag;
            //leftOver *= 1 - Vector2.Dot(hit.normal, velocityInit);
            Debug.Log("left "+leftOver);
            return snapToSurface + collideAndSlide(pos+snapToSurface, leftOver, depth - 1, velocityInit);
        }

        return vel;
    }
}
