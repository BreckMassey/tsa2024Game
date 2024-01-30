using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigQueenBoss : BossParent
{

    public GameObject[] walkingPawnPrefab;
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

    float startDelay = 3;


    public GameObject winScreen;

    public string playerPrefLevelName;

    public Transform[] positions;
    int currentPos;
    public float moveSpeed;

    public BoxCollider2D mainCollider;

    public LayerMask playerLayer;

    public Transform shootSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        health = stageHealths[0];
        smoothHealth = 0;
        player = GameObject.FindGameObjectWithTag("player");

        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;
        smoothHealth -= (smoothHealth - health) * Time.deltaTime * 3;
        if (startDelay <= 0)
        {
            if (health < 0)
            {
                stage = Mathf.Min(stage, -1);
                healthSlider.value = 0;
                PlayerPrefs.SetInt(playerPrefLevelName, 1);
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
                spawnPawns(8);
                moving();
                moveSpeed = 15;
                //shootCheckers();
                //spawnFallingCheckers();
            }
            if (stage == 1)
            {
                moveSpeed = 10;
                spawnPawns(5);
                moving();
                shootCheckers();
                //spawnFallingCheckers();
            }
            if (stage == 2)
            {
                spawnPawns(2);
                moving();
                shootCheckers();
                spawnFallingCheckers();
            }



            walkingPawnSpawnDelay -= Time.deltaTime;
            fallingCheckerSpawnDelay -= Time.deltaTime;
            shootingDelay -= Time.deltaTime;
        }
        else
        {
            health = stageHealths[0];
            healthSlider.value = smoothHealth / stageHealths[0];
        }

        checkForPlayer();
    }
    void checkForPlayer()
    {
        if (Physics2D.OverlapBox(mainCollider.transform.position, mainCollider.size, 0, playerLayer))
        {
            player.GetComponent<PlayerController2>().takeDamage();
        }


    }
    void spawnPawns(int count)
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");
        int pawnCount = 0;
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].layer == (pawnLayer))
            {
                pawnCount++;

            }
        }
        if (pawnCount < count && walkingPawnSpawnDelay <= 0)
        {
            GameObject spawnedPawn = Instantiate(walkingPawnPrefab[(int)Random.Range(0, walkingPawnPrefab.Length)], new Vector3(Random.Range(0.0f, 1.0f) > 0.5 ? -30 : 30, -3.4f + 3 * Mathf.Floor(Random.Range(0, 4)), 11), Quaternion.identity);

            walkingPawnSpawnDelay = Random.Range(0.4f, 3);
            if (stage >= 1)
            {
                walkingPawnSpawnDelay = Random.Range(0.8f, 2);
                //spawnedPawn.GetComponent<PawnBoss>().setJump(Random.Range(0, 10) > 3);
            }
        }

    }

    void spawnFallingCheckers()
    {

        if (fallingCheckerSpawnDelay <= 0)
        {
            float x = ((int)Random.Range(0, 3)) * 6 - 6;
            GameObject spawnedPawn = Instantiate(fallingChecker, new Vector3(x,20,0), Quaternion.identity);
            fallingCheckerSpawnDelay = Random.Range(1, 3);
            if (stage >= 2)
            {
                fallingCheckerSpawnDelay = Random.Range(3, 4);
            }
        }

    }

    void shootCheckers()
    {
        if (shootingDelay <= 0)
        {
            float angle = Vector2.SignedAngle(transform.right, player.transform.position - shootSpawnPoint.position);
            GameObject checkerSpawned = Instantiate(shootingChecker, shootSpawnPoint.position, Quaternion.Euler(0, 0, angle));
            checkerSpawned.GetComponent<EnemyCheckerMovement>().speed = 8;
            if (stage > 1)
            {
                checkerSpawned.GetComponent<EnemyCheckerMovement>().rotationSpeed = 60;
            }
            shots++;
            shootingDelay = 0.3f;
            if (shots >= 3 || (stage == 1 && shots >= 2) || (stage > 1 && shots >= 1))
            {
                shots = 0;
                shootingDelay = Random.Range(5, 10);
            }
        }


    }

    void moving()
    {
        if (Vector2.Distance(transform.position, positions[currentPos].position) < 0.1f)
        {
            List<Transform> possibleLocations = new List<Transform>();
            List<int> possibleLocationsI = new List<int>();
            for (int i = 0; i < positions.Length; i++)
            {
                Vector2 dirToSpot = (positions[currentPos].position - positions[i].position);
                dirToSpot.Normalize();
                dirToSpot.x = Mathf.Abs(dirToSpot.x);
                dirToSpot.y = Mathf.Abs(dirToSpot.y);
                if (i != currentPos)// && ((dirToSpot.x >= 1.0 && dirToSpot.y <= 0.0) || (dirToSpot.y >= 1.0 && dirToSpot.x <= 0.0)))
                {
                    possibleLocations.Add(positions[i]);
                    possibleLocationsI.Add(i);
                }
            }
            currentPos = possibleLocationsI[Random.Range(0, possibleLocationsI.Count)];

        }
        Vector2 moveDir = -(transform.position - positions[currentPos].position);
        moveDir.Normalize();
        transform.Translate(moveSpeed * Time.deltaTime * moveDir);
    }

}
