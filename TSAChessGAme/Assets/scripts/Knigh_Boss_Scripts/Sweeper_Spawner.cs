using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper_Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval;
    public float spawnCount;
    float spawnTimer=10;
    public bool spawning;
    void Start()
    {
       // StartCoroutine(SpawnAtRandomIntervals());
    }

    public void Update()
    {
        spawnTimer -= Time.deltaTime;


        if (spawning&&spawnTimer<=0) {
            spawnTimer = spawnInterval* Random.Range(0.5f, 2f);


            Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        }

    }
    IEnumerator SpawnAtRandomIntervals()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f)); 

            Instantiate(objectToSpawn, transform.position, Quaternion.identity); 
            Debug.Log("DONE!");
        }
    }
}
