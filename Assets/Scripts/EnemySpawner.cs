using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Waves wavesScript;
    
    void Start()
    {
        wavesScript.waveEvent += HandleSpawn;
    }

    void Update()
    {

    }

    public void Spawn(int spawnAmountMin, int spawnAmountMax) {
        int spawnAmount = UnityEngine.Random.Range( spawnAmountMin, spawnAmountMax + 1 );

        List<int> currentSpawns = new List<int>();

        for( int i = 0; i < spawnAmount; i++ ) {
            int spawn;
            spawn = UnityEngine.Random.Range( 0, spawnPoints.Length );
            while( currentSpawns.Contains( spawn ) ) {
                spawn = UnityEngine.Random.Range( 0, spawnPoints.Length );
            }

            GameObject newEnemy = Instantiate( enemyPrefab, spawnPoints[spawn].position, spawnPoints[spawn].rotation );
            currentSpawns.Add( spawn );
        }
    }

    public void HandleSpawn(int waveNumber) {
        if( waveNumber == 10 ) {
            Spawn( 10, 10 );
            return;
        }

        int spawnAmountMin = Mathf.Min( 10, Mathf.RoundToInt( Mathf.Pow( 1.2f, waveNumber ) ) );
        int spawnAmountMax = Mathf.Min( 10, spawnAmountMin + Mathf.RoundToInt( Mathf.Pow( 1.05f, waveNumber ) ) );
        Spawn( spawnAmountMin, spawnAmountMax );
    }

}
