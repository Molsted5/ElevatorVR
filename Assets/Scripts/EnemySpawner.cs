using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; 
    
    void Start()
    {
        int spawnAmountMin = 1;
        int spawnAmountMax = 3;
        int spawnAmount = UnityEngine.Random.Range(spawnAmountMin, spawnAmountMax + 1); 

        List<int> currentSpawns = new List<int>();

        for (int i = 0; i < spawnAmount; i++) {
            int spawn;
            do {
                spawn = UnityEngine.Random.Range(0, spawnPoints.Length);
            } while (currentSpawns.Contains(spawn));

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[spawn].position, spawnPoints[spawn].rotation);
            currentSpawns.Add(spawn);
        }

    }

    void Update()
    {

    }

}
