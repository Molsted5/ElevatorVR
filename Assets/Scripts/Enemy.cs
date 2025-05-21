using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform playerTransform;

    public AudioSource source;
    public AudioClip criticalHit;

    public Shoot playerScript;

    public int health = 90;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript.hitEvent += Hit;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        { 
            enemyAgent.SetDestination(playerTransform.position); 
        }

    }

    public void Hit(RaycastHit hitInfo)
    {
        GameObject enemyOBJ = hitInfo.transform.gameObject;

        if(gameObject == enemyOBJ)
        {
            int damageTaken = UnityEngine.Random.Range(20, 50);

            if(damageTaken >= 39)
            {
                source.clip = criticalHit;        
                source.Play();
            }

            health = health - damageTaken;


           if(health <= 0)
            {
                Die();
            }


        }



    }

    public void Die()
    {
        playerScript.hitEvent -= Hit;
        Destroy(gameObject);

    }





}
