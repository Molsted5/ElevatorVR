using UnityEngine;
using System.Collections;

public class Waves: MonoBehaviour {
    public delegate void WaveDelegate( int _waveNumber, int _midWaveSpawn = 1, string _waveType = "wave" );
    public event WaveDelegate waveEvent;

    public int waveNumber;
    public EnemySpawner enemySpawner;
    private int midWaySpawn;

    void Awake() {
        Enemy.deathEvent += HandleEnemyDeath;
    }

    void Start() {
        waveNumber = 1;
        StartCoroutine( InvokeWaveEventNextFrame() );
        enemySpawner.allEnemiesSpawnedEvent += HandleAllEnemiesSpawned;
    }

    IEnumerator InvokeWaveEventNextFrame() {
        yield return null; // wait one frame so subscribers can register
        Debug.Log( $"Waves: Invoking waveEvent for wave {waveNumber}" );
        waveEvent?.Invoke( waveNumber );
    }

    private void HandleEnemyDeath( Enemy enemy ) {
        if( Enemy.enemyCount == 1 ) {
            waveNumber++;
            Debug.Log( $"Waves: All enemies defeated. Advancing to wave {waveNumber}" );
            waveEvent?.Invoke( waveNumber );
            return;
        }

        if( Enemy.enemyCount <= midWaySpawn ) {
            Debug.Log( $"Waves: Mid-wave triggered. Remaining enemies: {Enemy.enemyCount}" );
            waveEvent?.Invoke( waveNumber, midWaySpawn, "MidWave" );
        }
    }

    private void HandleAllEnemiesSpawned( int spawnAmount, float enemiesRemaining01 ) {
        midWaySpawn = Mathf.RoundToInt( enemiesRemaining01 * spawnAmount );
        Debug.Log( $"Waves: Mid-wave threshold set to {midWaySpawn} of {spawnAmount} enemies" );
    }

    void OnDestroy() {
        Enemy.deathEvent -= HandleEnemyDeath;

        if( enemySpawner != null )
            enemySpawner.allEnemiesSpawnedEvent -= HandleAllEnemiesSpawned;
    }
}
