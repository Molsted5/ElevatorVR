using UnityEngine;

public class Waves : MonoBehaviour
{
    public delegate void WaveDelegate(int _waveNumber, int _midWaveSpawn = 1, string _waveType = "wave" );
    public event WaveDelegate waveEvent;
    public int waveNumber;
    [SerializeField] private EnemySpawner enemySpawner;
    private int midWaySpawn;

    void Start()
    {
        Enemy.deathEvent += HandleEnemyDeath; // because static event, no enemy instance reference needed (enemy instance is passed along with broadcast)
        waveNumber = 1;
        waveEvent?.Invoke( waveNumber );
        enemySpawner.allEnemiesSpawnedEvent += HandleAllEnemiesSpawned;
    }

    void Update()
    {
        
    }

    private void HandleEnemyDeath( Enemy enemy ) {
        if( Enemy.enemyCount == 1 ) {
            waveNumber++;
            waveEvent?.Invoke( waveNumber );
            return;
        }
        if( Enemy.enemyCount <= midWaySpawn ) {
            waveEvent?.Invoke( waveNumber, midWaySpawn, "MidWave" );
        }
    }

    private void HandleAllEnemiesSpawned( int spawnAmount, float enemiesRemaining01 ) {
        midWaySpawn = Mathf.RoundToInt( enemiesRemaining01 * spawnAmount );
    }
}
