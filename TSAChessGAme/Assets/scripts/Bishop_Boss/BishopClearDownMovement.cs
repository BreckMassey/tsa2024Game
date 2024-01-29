using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopClearDownMovement : MonoBehaviour
{
    public float speed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
