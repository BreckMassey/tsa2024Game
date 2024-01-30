using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShake : MonoBehaviour
{
    public float speed;
    public float xOffsetAffect;
    public float height;
    float startY;
    float sinTime;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        sinTime += Time.deltaTime * speed;
        transform.transform.position = new Vector3(transform.position.x, startY+Mathf.Abs(Mathf.Sin(sinTime+transform.position.x*xOffsetAffect)*height), transform.position.z);
    }
}
