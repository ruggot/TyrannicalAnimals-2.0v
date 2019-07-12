using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    public float roundTimer = 400f;
    public float countDowner = 3f;

    private int playerOnePoints;
    private int playerTwoPoints;

    // place holder health veribals
    private int playerOneHealth;
    private int playerTwoHealth;

    public int[] roundCount;

    private PlayerController chickenMovement;

    void Awake()
    {
        // sets player points to 0
        playerOnePoints = 0;
        playerTwoPoints = 0;

        // refrence the playermovement script
        chickenMovement = (PlayerController)FindObjectOfType(typeof(PlayerController));

        // disables the movement script on the playes
        chickenMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        countDowner -= Time.deltaTime;
        
        if (countDowner < 0)
        {
            //count down timer for start of rounds
            roundTimer -= Time.deltaTime;
            chickenMovement.enabled = true;
        }

        if (playerOneHealth <=0)
        {
            chickenMovement.enabled = false;
            playerOnePoints++;
            SceneManager.LoadScene("level_1");
        }

        if (playerTwoHealth <=0)
        {
            chickenMovement.enabled = false;
            playerTwoPoints++;
            SceneManager.LoadScene("Level_1");
        }

        if (playerOnePoints <= 3)
        {
            WinGame();
            // add UI for this that says that player won this round
        }

        if (playerTwoPoints <= 3)
        {
            WinGame();
            // add UI for this that says that player won this round
        }

        void WinGame()
        {
            chickenMovement.enabled = true;
        }
    }
}
