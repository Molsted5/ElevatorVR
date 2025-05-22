using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHealth = 100;

    private Coroutine invunabilityCoroutine;

    public bool canTakeDMG = true;

    public float invunabilityTime;


    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Target") && canTakeDMG)
        {
            StartInvunabilityCoroutione();
        }
        
    }


    private void DMGTaken()
    {

        int damageTaken = UnityEngine.Random.Range(20, 50);

        playerHealth = playerHealth - damageTaken;



        if(playerHealth <= 1)
        {
           // GameLost();

        }



    }

    private void GameLost()
    {


    }


    public void StartInvunabilityCoroutione()
    {
        if (invunabilityCoroutine != null)
        {
            StopCoroutine(invunabilityCoroutine);
        }
        StartCoroutine(Invunability());

    }

    private IEnumerator Invunability()
    {
        canTakeDMG = false;
        
        DMGTaken();
        Debug.Log(playerHealth);

        yield return new WaitForSeconds(invunabilityTime);
        canTakeDMG = true;

    }


}
