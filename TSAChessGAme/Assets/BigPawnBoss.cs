using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPawnBoss : BossParent
{

    public GameObject walkingPawnPrefab;
    public int pawnLayer;
    GameObject player;
    float walkingPawnSpawnDelay;

    public GameObject fallingChecker;
    float fallingCheckerSpawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");   
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            spawnPawns();
            spawnFallingCheckers();
        }
        walkingPawnSpawnDelay -= Time.deltaTime;
        fallingCheckerSpawnDelay -= Time.deltaTime;
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
        }
    
    }



    void spawnFallingCheckers()
    {
        
        if (fallingCheckerSpawnDelay <= 0)
        {
            float x = ((int)Random.Range(0, 3))*6-6;
            GameObject spawnedPawn = Instantiate(fallingChecker, new Vector3(x, 12, 0), Quaternion.identity);
            fallingCheckerSpawnDelay = Random.Range(1, 3);
        }

    }

}
