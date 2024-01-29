using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelSelect : MonoBehaviour
{

    public int level;
    public BoxCollider2D[] passThroughColliders;
    public SpriteRenderer[] passThroughSprites;
    public LayerMask playerLayer;
    bool playerInArea;
    public string bossName;
    public GameObject instructionMenu;
    public TextMeshProUGUI titleText;
    public string playerPrefLevelName;
    // Start is called before the first frame update
    void Start()
    {
        if (playerPrefLevelName != "")
        {
            if (PlayerPrefs.GetInt(playerPrefLevelName) == null)
            {
                PlayerPrefs.SetInt(playerPrefLevelName, 0);
            }
            if (PlayerPrefs.GetInt(playerPrefLevelName) > 0)
            {
                for (int i = 0; i < passThroughColliders.Length; i++)
                {
                    passThroughColliders[i].enabled = false;
                }
                for (int i = 0; i < passThroughSprites.Length; i++)
                {
                    passThroughSprites[i].enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log( Input.GetKeyUp(KeyCode.Space));
        if (playerInArea && Input.GetKeyUp(KeyCode.Space)) {
            SceneManager.LoadScene(level);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("error");
        playerInArea = true;
        instructionMenu.SetActive(true);
        instructionMenu.GetComponent<endScreenEffects>().expand();
        titleText.text = bossName;

    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInArea = false;
        instructionMenu.SetActive(true);
        instructionMenu.GetComponent<endScreenEffects>().contract();
        titleText.text = bossName;

    }
}
