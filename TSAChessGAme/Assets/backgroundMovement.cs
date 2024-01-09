using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{

    public float tileSize;
    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(fMod(Time.time * speed.x, tileSize), Mathf.Sin(Time.time * speed.y) * tileSize, transform.position.z);
        //  Debug.Log((Time.time * speed.x)+"%"+ tileSize + "="+fMod(Time.time * speed.x, tileSize));
    }

    float fMod(float a, float b)
    {
        return a - Mathf.Floor(a / b) * b;
    }

}
