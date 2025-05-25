using UnityEngine;
using TMPro;

public class UIScreen : MonoBehaviour
{
    public TMP_Text TextUI;
    public Player playerScript;
    public ScoreManager scoreScript;
    public Waves waveScript;
    int playerHealth;
    int playerScore;
    int waveNr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScreen();




    }

    // Update is called once per frame
    void Update()
    {
      

        
    }

    public void UpdateScreen()
    {
        waveNr = waveScript.waveNumber;
        playerHealth = playerScript.currentHealth;
        playerScore = scoreScript.currentScore;
        TextUI.text = 
            "Wave: " + waveNr.ToString() + "<br><br>" + 
            "Current health: " + playerHealth.ToString() + "<br><br>" + 
            "Current Score: " + playerScore.ToString();


    }

 



}
