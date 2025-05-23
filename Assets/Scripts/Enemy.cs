using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform playerTransform;

    public AudioSource source;
    public AudioClip criticalHit;

    public Shoot shootScript;
    public ScoreManager scoreScript;

    public GameObject player;
    public GameObject scoreManager;

    public int health = 90;

    public ParticleSystem bloodsplosion;

    public delegate void DeathDelegate(Enemy enemy);
    public static event DeathDelegate deathEvent;

    public static int enemyCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount++;
        shootScript = player.GetComponent<Shoot>();
        scoreScript = scoreManager.GetComponent<ScoreManager>();
        shootScript.hitEvent += Hit;
        playerTransform = GameObject.Find("Main Camera").transform;  
    }

    // Update is called once per frame
    void Update()
    {
        print("count: " +  enemyCount);
        if (gameObject != null)
        { 
            enemyAgent.SetDestination(playerTransform.position); 
        }
       
    }

    public void Hit(GameObject hitInfo)
    {   
        GameObject enemyOBJ = hitInfo;

        if(gameObject == enemyOBJ)
        {
           

            bloodsplosion.Play();
           
            int damageTaken = UnityEngine.Random.Range(20, 50);

            if(damageTaken >= 39)
            {
                source.clip = criticalHit;        
                source.Play();
            }

            health = health - damageTaken;

            if(health <= 0)
            {
                scoreScript.Score();
                Debug.Log(scoreScript.currentScore);
                bloodsplosion.Play();
                Die();
            }

        }
    }

    public void Die()
    {
        enemyCount--;
        shootScript.hitEvent -= Hit;
        deathEvent?.Invoke( this ); // Broadcast globally (static event)
        Destroy( gameObject );
    }

}
