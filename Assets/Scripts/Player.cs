using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHealth = 100;


    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {


        
    }


    private void DMGTaken()
    {

        int damageTaken = UnityEngine.Random.Range(20, 50);

        playerHealth = playerHealth - damageTaken;



        if(playerHealth <= 1)
        {
            GameLost();

        }



    }

    private void GameLost()
    {


    }


}
