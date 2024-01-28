using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BigPawnBoss : BossParent
{

    public GameObject walkingPawnPrefab;
    public int pawnLayer;
    GameObject player;
    float walkingPawnSpawnDelay;

    public GameObject fallingChecker;
    float fallingCheckerSpawnDelay;

    public GameObject shootingChecker;
    float shootingDelay;
    int shots;
    public GameObject deathParticle;
    public float[] stageHealths;

    public SpriteRenderer sprite;

    public Slider healthSlider;

    public float smoothHealth;

    float startDelay=5;


    public GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        health = stageHealths[0];
        smoothHealth = health;
        player = GameObject.FindGameObjectWithTag("player");

        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;
        smoothHealth -= (smoothHealth - health) * Time.deltaTime*3;
        if (startDelay <= 0)
        {
            if (health < 0)
            {
                stage = Mathf.Min(stage, -1);
                healthSlider.value = 0;
            }
            else
            {

                for (int i = 0; i < stageHealths.Length; i++)
                {
                    if (health < stageHealths[i])
                    {
                        stage = i;
                    }
                }

                healthSlider.value = smoothHealth / stageHealths[0];
            }

            if (stage == -1)
            {
                for (int i = 0; i < 100; i++)
                {
                    Instantiate(deathParticle, transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-4, 4) - 3, 0), Quaternion.identity);
                }
                sprite.enabled = false;
                stage = -2;
                player.GetComponent<PlayerController2>().invincibility = 10000;

                winScreen.SetActive(true);
            }

            if (stage == 0)
            {
                spawnPawns();
                shootCheckers();
                //spawnFallingCheckers();
            }
            if (stage == 1)
            {
                spawnPawns();
                shootCheckers();
                spawnFallingCheckers();
            }
            if (stage == 2)
            {
                spawnPawns();
                shootCheckers();
                spawnFallingCheckers();
            }



            walkingPawnSpawnDelay -= Time.deltaTime;
            fallingCheckerSpawnDelay -= Time.deltaTime;
            shootingDelay -= Time.deltaTime;
        }
        else {
            health = stageHealths[0];
        }
    }

    void spawnPawns() {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        int pawnCount = 0;
        for (int i = 0; i < allEnemies.Length; i++) {
            if (allEnemies[i].layer==(pawnLayer)) {
                pawnCount++;

            }
        }
        if (pawnCount < 5&&walkingPawnSpawnDelay<=0) {
            GameObject spawnedPawn=   Instantiate(walkingPawnPrefab, new Vector3(Random.Range(0.0f, 1.0f) > 0.5 ? -30 : 30,-3.4f, 11), Quaternion.identity);

            walkingPawnSpawnDelay = Random.Range(0.4f,3);
            if (stage >= 1)
            {
                walkingPawnSpawnDelay = Random.Range(0.8f, 2);
                spawnedPawn.GetComponent<PawnBoss>().setJump(Random.Range(0,10)>3);
            }
        }
    
    }

    void spawnFallingCheckers()
    {
        
        if (fallingCheckerSpawnDelay <= 0)
        {
            float x = ((int)Random.Range(0, 3))*6-6;
            GameObject spawnedPawn = Instantiate(fallingChecker, new Vector3(x, 12, 0), Quaternion.identity);
            fallingCheckerSpawnDelay = Random.Range(1, 3);
            if (stage >= 2)
            {
                fallingCheckerSpawnDelay = Random.Range(3, 4);
            }
        }

    }

    void shootCheckers() {
        if (shootingDelay<=0) {
            float angle= Vector2.SignedAngle(transform.right, player.transform.position - new Vector3(12, 1, -1));
            GameObject checkerSpawned=  Instantiate(shootingChecker, new Vector3(12,1,-1), Quaternion.Euler(0,0,angle));
            if (stage > 1) {
                checkerSpawned.GetComponent<EnemyCheckerMovement>().rotationSpeed = 60;
            }
            shots++;
            shootingDelay = 0.3f;
            if (shots >= 3||(stage==1&&shots>=2)||(stage>1&&shots>=1)) {
                shots = 0;
                shootingDelay = Random.Range(3, 6);
            }
        }


    }


}
