using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StorySpeaking : MonoBehaviour
{

    public string stringToSpeak;
    public int letterOn;
    public RectTransform speakerPos;
    float yPos;

    public TextMeshProUGUI uiText;
    public float speakingSpeed;
    float speakingCountDown;
    public float bounceSpeed;
    public float bounceHeight; 
    public float bounceCount;
    // Start is called before the first frame update
    void Start()
    {
        yPos = speakerPos.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bounceCount += Time.deltaTime;

        speakerPos.position = new Vector3(speakerPos.position.x, yPos+Mathf.Abs(Mathf.Sin(bounceCount*bounceSpeed)*bounceHeight), speakerPos.position.z);

        speakingCountDown -= Time.deltaTime;
        if (speakingCountDown <= 0&& letterOn<= stringToSpeak.Length-1)
        {
            speakingCountDown = speakingSpeed;
            letterOn++;
            string outputText = "";
            for (int i = 0; i < letterOn; i++)
            {
                outputText += stringToSpeak[i];
            }
            uiText.text = outputText;
        }
    }
}
