using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float enemiesToSpawn;
    public int round;

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        EnemyKilled();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void EnemyKilled()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            round++;
            enemiesToSpawn = enemiesToSpawn * 1.2f;
            SpawnEnemies();
        }
    }
}
