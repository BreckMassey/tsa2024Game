using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform[] follow;
    public Vector3[] setPos;
    public float percent;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = new Vector3(0, 0, 0);
        for (int i = 0; i < follow.Length; i++) {
            followPos += follow[i].position;
        }

        for (int i = 0; i < setPos.Length; i++)
        {
            followPos += setPos[i];
        }
        followPos /= follow.Length+ setPos.Length;
        transform.Translate((transform.position + offset - (followPos)) * -1 * percent * Time.deltaTime);
    }

}
