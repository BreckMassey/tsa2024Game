using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endScreenEffects : MonoBehaviour
{

    float delay = 1;
    public RectTransform rectTransform;
    float height = 0;
    float vHeight = 100;
    public float desiredHeight;
    bool expanding = true;
    public float desiredDelay;
    // Start is called before the first frame update
    void Start()
    {
        if (desiredHeight == 0) {
            desiredHeight = 2000;
        }
        delay = desiredDelay;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,0);
    }

    void Awake()
    {
        if (desiredHeight == 0)
        {
            desiredHeight = 2000;
        }
        delay = desiredDelay;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
    }
    // Update is called once per frame
    void Update()
    {
       // Debug.Log(delay + " " + expanding+" "+Time.deltaTime+" "+Time.timeScale);

        if (SceneManager.GetActiveScene().name == "Menu") {
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);

        }

        delay -= Time.deltaTime;
        if (expanding)
        {
            if (delay < 0 && height < desiredHeight)
            {
                vHeight += 2000 * Time.deltaTime;
                height += vHeight * Time.deltaTime;
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            }
        }
        else {
            if (delay < 0 && height > 0)
            {
                vHeight -= 2000 * Time.deltaTime;
                height += vHeight * Time.deltaTime;
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            }
        }
    }
    public void contract()
    {
        if (expanding)
        {
            vHeight = -100;
            expanding = false;
            height = Mathf.Min(height, desiredHeight);
        }
    }

    public void expand()
    {
        if (!expanding)
        {
            vHeight = 100;
            expanding = true;
            height = Mathf.Max(height, 0);
        }
    }



}
