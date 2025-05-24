using UnityEngine;
using System.Collections;

public class Waves: MonoBehaviour {
    public delegate void WaveDelegate( int _waveNumber, int _midWaveSpawn = 1, string _waveType = "wave" );
    public event WaveDelegate waveEvent;

    public int waveNumber;
    public EnemySpawner enemySpawner;
    private int midWaySpawn;

    void Awake() {
        // Subscribe to the static death event early
        Enemy.deathEvent += HandleEnemyDeath;
    }

    IEnumerator Start() {
        waveNumber = 1;

        // Wait one frame to ensure other scripts have subscribed
        yield return null;

        Debug.Log( "Invoking initial wave event" );
        waveEvent?.Invoke( waveNumber );

        // Subscribe to allEnemiesSpawnedEvent after waveEvent is fired
        enemySpawner.allEnemiesSpawnedEvent += HandleAllEnemiesSpawned;
    }

    private void HandleEnemyDeath( Enemy enemy ) {
        if( Enemy.enemyCount == 1 ) {
            waveNumber++;
            Debug.Log( $"All enemies defeated. Starting wave {waveNumber}" );
            waveEvent?.Invoke( waveNumber );
            return;
        }

        if( Enemy.enemyCount <= midWaySpawn ) {
            Debug.Log( $"Mid-wave triggered at enemy count: {Enemy.enemyCount}" );
            waveEvent?.Invoke( waveNumber, midWaySpawn, "MidWave" );
        }
    }

    private void HandleAllEnemiesSpawned( int spawnAmount, float enemiesRemaining01 ) {
        midWaySpawn = Mathf.RoundToInt( enemiesRemaining01 * spawnAmount );
        Debug.Log( $"Mid-wave spawn threshold set to {midWaySpawn} based on {spawnAmount} enemies" );
    }
}
