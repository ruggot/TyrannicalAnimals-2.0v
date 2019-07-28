using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    // countdown and round timer
    [SerializeField] private float roundTimer = 400f;
    [SerializeField] private float countDownTimer = 3f;

    // points for the players
    private static int playerOnePoints; // I made these static because they are variables that should remain the same game-wide
    private static int playerTwoPoints;

    // placeholder health veriables
    private float playerOneHealth;
    private float playerTwoHealth;
    // round counter array
    protected int[] roundCount;

    // removed function achieved by Player.cs

    /// Removed PlayerController variable as its function in this script can be achieved by toggling bool SceneScript.inGame
    //  // refrences the playermovement
    //   private PlayerController chickenMovement;

    void Start()
    {
        // sets player points to 0
        playerOnePoints = 0;
        playerTwoPoints = 0;    // sets player health to 1
        playerOneHealth = 1f;
        playerTwoHealth = 1f;   // refrence the playermovement script
    }    // Update is called once per frame


    void Update()
    {
        countDownTimer -= Time.deltaTime;

        if (countDownTimer < 0)
        {
            //count down timer for start of rounds
            roundTimer -= Time.deltaTime;
            SceneScript.inGame = true;
        }
        if (playerOneHealth <= 0)
        {
            SceneScript.inGame = false;
            playerOnePoints++;
            SceneManager.LoadScene("level_1");
        }
        if (playerTwoHealth <= 0)
        {
            SceneScript.inGame = false;
            playerTwoPoints++;
            SceneManager.LoadScene("Level_1");
        }
        if (playerOnePoints >= 3)
        {
            WinGame(1);
            // add UI for this that says that player won this round
        }
        if (playerTwoPoints >= 3)
        {
            WinGame(2);
            // add UI for this that says that player won this round
        }
        void WinGame(int player)
        {
            SceneScript.inGame = false;
            switch (player)
            {
                case 1: /* player 1 wins */ break;
                case 2: /* player 2 wins */ break;
                default: break;
            }
        }
    }
}
