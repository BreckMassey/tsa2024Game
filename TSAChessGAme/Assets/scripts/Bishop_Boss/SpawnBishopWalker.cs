using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBishopWalker : MonoBehaviour
{
    public GameObject bishopPrefab; 
    public float spawnInterval = 10f;
    public bool spawning;
    float spawnCountdown;
    private void Start()
    {
       // StartCoroutine(SpawnEnemyRoutine());
    }
    private void Update()
    {
        spawnCountdown -= Time.deltaTime;
        if (spawning && spawnCountdown <= 0)
        {
            Instantiate(bishopPrefab, transform.position, Quaternion.identity);
            spawnCountdown = spawnInterval * Random.Range(0.5f, 2);
        }
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
