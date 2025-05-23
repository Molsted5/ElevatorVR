using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentHealth = 100;

    private Coroutine invunabilityCoroutine;

    public bool canTakeDMG = true;

    public float invunabilityTime;


    public AudioSource source;
    public AudioClip[] clips;



    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {


        
    }


    public void DMGTaken()
    {


        int damageTaken = 2;

        currentHealth = currentHealth - damageTaken;


        source.clip = clips[0];
        source.Play();
        source.pitch = UnityEngine.Random.Range(.7f, 1f);
        



        if (currentHealth <= 1)
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
       

        yield return new WaitForSeconds(invunabilityTime);
        canTakeDMG = true;

    }


}
