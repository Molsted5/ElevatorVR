using UnityEngine;

public class Waves : MonoBehaviour
{
    public delegate void WaveDelegate(int _waveNumber);
    public event WaveDelegate waveEvent;
    public int waveNumber;

    void Start()
    {
        Enemy.deathEvent += HandleEnemyDeath; // because static event, no enemy instance reference needed (enemy instance is passed along with broadcast)
        waveNumber = 1;
        waveEvent?.Invoke( waveNumber );
    }

    void Update()
    {
        
    }

    private void HandleEnemyDeath( Enemy enemy ) {
        if( Enemy.enemyCount == 1 ) {
            waveNumber++;
            waveEvent?.Invoke( waveNumber );
        }
    }
}
