using UnityEngine;
using TMPro;

public class UIScreen : MonoBehaviour
{
    public TMP_Text ScoreandHealth;
    public Player playerscript;
    int playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = playerscript.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreandHealth.text = "Current health: " + playerHealth.ToString();
    }
}
