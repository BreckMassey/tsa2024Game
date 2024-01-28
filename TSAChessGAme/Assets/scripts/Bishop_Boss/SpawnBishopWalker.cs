using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBishopWalker : MonoBehaviour
{
    public GameObject bishopPrefab; 
    public float spawnInterval = 10f; 

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(bishopPrefab, transform.position, Quaternion.identity);
    }
}
