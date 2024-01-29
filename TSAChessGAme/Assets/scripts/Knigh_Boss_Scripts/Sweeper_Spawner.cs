using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper_Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; 

    void Start()
    {
        StartCoroutine(SpawnAtRandomIntervals());
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
