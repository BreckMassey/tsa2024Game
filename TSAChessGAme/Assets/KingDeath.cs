using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KingDeath : MonoBehaviour
{
    public RectTransform kingTransform;
    public Image kingImage;
    public Color aliveColor;
    public Color deathColor;
    float angle = 0;
    float vAngle = 0;
    public Vector3 startPos;
    public Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(angle) < 80)
        {
            angle -= vAngle;
            vAngle += Time.deltaTime*0.5f;
        }
        kingImage.color=Color.Lerp(aliveColor, deathColor, Mathf.Abs(angle) / 80);

        kingTransform.SetLocalPositionAndRotation(Vector3.Lerp(startPos,endPos, Mathf.Abs(angle) / 80), Quaternion.Euler(0, 0, angle));
    }
}
