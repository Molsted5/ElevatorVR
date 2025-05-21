using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; 
    
    void Start()
    {
        int spawnAmountMin = 1;
        int spawnAmountMax = 3;
        int spawnAmount = Random.Range(spawnAmountMin, spawnAmountMax + 1); 

        int currentSpawn=0;
        for(int i = 0; i < spawnAmount; i++ ) {
            int spawn;
            do {
                spawn = Random.Range(0, spawnPoints.Length); 
            } while(spawn == currentSpawn);

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[spawn].position, spawnPoints[spawn].rotation);
            currentSpawn = spawn;
        }
    }

    void Update()
    {

    }

}
