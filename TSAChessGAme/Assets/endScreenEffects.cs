using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endScreenEffects : MonoBehaviour
{

    float delay = 1;
    public RectTransform rectTransform;
    float height = 0;
    float vHeight = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0&&height<2000) {
            vHeight += 2000* Time.deltaTime;
            height += vHeight * Time.deltaTime;
            rectTransform.sizeDelta=new Vector2(500, height);
        }
    }
}
