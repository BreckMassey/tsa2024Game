using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("playerMapX") == null || PlayerPrefs.GetInt("playerMapY") == null) {

            PlayerPrefs.SetInt("playerMapX", (int)transform.position.x);
            PlayerPrefs.SetInt("playerMapY", (int)transform.position.y);
        }

       transform.position=new Vector2(PlayerPrefs.GetInt("playerMapX"), PlayerPrefs.GetInt("playerMapY"));
    }

    // Update is called once per frame
    void Update()
    {
        //rb.transform.Translate(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"))*movementSpeed*Time.deltaTime);
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * movementSpeed;
        PlayerPrefs.SetInt("playerMapX", (int)transform.position.x);
        PlayerPrefs.SetInt("playerMapY", (int)transform.position.y);
    }
}
