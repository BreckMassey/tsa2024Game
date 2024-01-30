using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    float delay = 1;
    public RectTransform rectTransform;
    float height = 0;
    float vHeight = 100;
    public float desiredHeight;
    bool expanding = false;
    public float desiredDelay;
    bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        if (desiredHeight == 0)
        {
            desiredHeight = 2000;
        }
        delay = desiredDelay;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
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
        if (Input.GetKeyUp(KeyCode.Escape)) {
            open= !open;
            if (open)
            {
                expand();
            }
            else {
                contract();
            }
        }

        if (open)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        delay -= Time.unscaledDeltaTime;
        if (expanding)
        {
            if (delay < 0 && height < desiredHeight)
            {
                vHeight += 4000 * Time.unscaledDeltaTime;
                height += vHeight * Time.unscaledDeltaTime;
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            }
        }
        else
        {
            if (delay < 0 && height > 0)
            {
                vHeight -= 10000 * Time.unscaledDeltaTime;
                height += vHeight * Time.unscaledDeltaTime;
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            }
        }
    }
    public void contract()
    {
        open = false;
        vHeight = -2000;
        expanding = false;
        height = Mathf.Min(height, desiredHeight);
    }

    public void expand()
    {
        open = true;
        vHeight = 100;
        expanding = true;
        height = Mathf.Max(height, 0);
    }

}
