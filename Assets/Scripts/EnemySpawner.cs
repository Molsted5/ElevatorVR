using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Waves wavesScript;
    public delegate void AllEnemiesSpawnedDelegate(int _spawnAmount, float _enemiesRemaining01);
    public event AllEnemiesSpawnedDelegate allEnemiesSpawnedEvent;
    public float enemiesRemaining01;
    public float waveScaleMin;
    public float waveScaleMax;
    public int maxSpawns;

    void Start()
    {
        wavesScript.waveEvent += HandleSpawn;
        maxSpawns = spawnPoints.Length;
    }

    void Update()
    {

    }

    public void Spawn(int spawnAmountMin, int spawnAmountMax ) {
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

        allEnemiesSpawnedEvent?.Invoke(spawnAmount, enemiesRemaining01);
    }

    public void HandleSpawn(int waveNumber, int midWaySpawn, string waveType) {
        if( waveType == "MidWave" ) {
            print(waveType);
            Spawn( midWaySpawn, midWaySpawn );
            return;
        }

        print(waveType + " " + waveNumber);

        if( waveNumber == 10 ) {
            Spawn( maxSpawns, maxSpawns );
            return;
        }

        int spawnAmountMin = Mathf.Min( maxSpawns, Mathf.RoundToInt( Mathf.Pow( waveScaleMin, waveNumber ) ) );
        int spawnAmountMax = Mathf.Min( maxSpawns, spawnAmountMin + Mathf.RoundToInt( Mathf.Pow( waveScaleMax, waveNumber ) ) );
        Spawn( spawnAmountMin, spawnAmountMax );
    }

}
