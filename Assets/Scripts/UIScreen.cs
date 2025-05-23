using UnityEngine;
using TMPro;

public class UIScreen : MonoBehaviour
{
    public TMP_Text TextUI;
    public Player playerscript;
    public ScoreManager scoreScript;
    int playerHealth;
    int playerScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

  
    }

    // Update is called once per frame
    void Update()
    {
      
        playerHealth = playerscript.currentHealth;
        playerScore = scoreScript.currentScore;
        TextUI.text = "Current health: " + playerHealth.ToString() + "<br><br>" + "Current Score: " + playerScore.ToString();
    }
}
