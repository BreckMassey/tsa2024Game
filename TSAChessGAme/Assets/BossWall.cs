using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossWall : BossParent
{

    public Slider healthSlider;

    public float smoothHealth;

    public float maxHealth;
    public GameObject deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smoothHealth -= (smoothHealth - health) * Time.deltaTime * 3;
        healthSlider.value = smoothHealth / maxHealth;

        if (health < 0)
        {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(deathParticle, transform.position + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            }
            healthSlider.value = 0;
            Destroy(gameObject);
        }

    }
}
