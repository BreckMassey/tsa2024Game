using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BishopBoss : BossParent
{
    public Slider healthSlider;

    public float smoothHealth;

    public float maxHealth;
    public GameObject deathParticle;

    public GameObject victoryScreen;

    public GameObject player;

    public float[] stageHealths;

    float startDelay = 2;

    public BishopClearMap bishopClearMap;
    public SpawnBishopWalker bishopWalker;
    public string playerPrefLevelName;

    // Start is called before the first frame update
    void Start()
    {

        health = stageHealths[0];
    }

    // Update is called once per frame
    void Update()
    {

        startDelay -= Time.deltaTime;
        smoothHealth -= (smoothHealth - health) * Time.deltaTime * 3;
        healthSlider.value = smoothHealth / maxHealth;
        if (startDelay <= 0)
        {

            if (health < 0)
            {
                PlayerPrefs.SetInt(playerPrefLevelName, 1);
                for (int i = 0; i < 100; i++)
                {
                    Instantiate(deathParticle, transform.position + new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
                }
                healthSlider.value = 0;
                victoryScreen.SetActive(true);
                if (player != null)
                {
                    player.GetComponent<PlayerController2>().invincibility = 1000000;
                }
                Destroy(gameObject);
            }
            for (int i = 0; i < stageHealths.Length; i++)
            {
                if (health < stageHealths[i])
                {
                    stage = i;
                }
            }
            if (stage == 0)
            {
                bishopWalker.spawning = true;
                bishopWalker.spawnInterval = 0.5f;

                bishopClearMap.spawning = false;
              //  knightShooter.shootingInterval = 1;
              // sweeper_Spawner.spawning = false;
            }
            if (stage == 1)
            {
                bishopWalker.spawning = true;
                bishopWalker.spawnInterval = 2.5f;

                bishopClearMap.spawning = true;
                bishopClearMap.spawnInterval = 6;

                // knightShooter.shootingInterval = 2f;
                // sweeper_Spawner.spawnInterval = 2f;
                //sweeper_Spawner.spawning = true;
            }
        }
        else
        {
            health = stageHealths[0];
        }
    }
}
