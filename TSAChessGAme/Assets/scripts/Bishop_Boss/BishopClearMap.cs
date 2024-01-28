using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopClearMap : MonoBehaviour
{
    public GameObject downBishopPrefab;
    public GameObject sideBishopPrefab;
    public float spawnInterval = 30f;
    public float bishopSpacing = 1.5f; // Spacing between bishops
    public int mapWidth = 10; // Horizontal spawn
    public int mapHeight = 10; // Vertical spawn
    public float horizontalOffset = 2.0f;
    public float verticalOffset = 2.0f;
    public int[] horizontalGapPositions = new int[3] {3, 6, 9}; // Horizontal gap positions
    public int[] verticalGapPositions = new int[3] {2, 5, 8}; // Vertical gap positions

    private void Start()
    {
        StartCoroutine(SpawnBulletRowRoutine());
    }

    private IEnumerator SpawnBulletRowRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            bool isVertical = Random.Range(0, 2) == 1;
            SpawnBulletRow(isVertical);
        }
    }

    private void SpawnBulletRow(bool isVertical)
    {
        int[] gapPositions = isVertical ? verticalGapPositions : horizontalGapPositions;
        int gapCenter = gapPositions[Random.Range(0, gapPositions.Length)];
        int mapSize = isVertical ? mapHeight : mapWidth;
        GameObject prefabToUse = isVertical ? sideBishopPrefab : downBishopPrefab;

        for (int i = 0; i < mapSize; i++)
        {
            if (i != gapCenter && i != gapCenter - 1 && i != gapCenter + 1)
            {
                Vector2 spawnPosition;
                if (isVertical)
                {
                    // For vertical spawning
                    spawnPosition = new Vector2(transform.position.x, i * bishopSpacing + verticalOffset);
                }
                else
                {
                    // For horizontal spawning
                    spawnPosition = new Vector2(i * bishopSpacing + horizontalOffset, transform.position.y);
                }
                Instantiate(prefabToUse, spawnPosition, Quaternion.identity);
            }
        }
    }
}
