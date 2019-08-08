using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManagementSystem : MonoBehaviour
{
    public static int player1Score = 2;
    public static int player2Score = 1;
    // Current round
    public static int currentRound;

    // max score a player can have
    public int maxPlayerWins = 3;
    // Max rounds that can be played
    public int maxRound = 6;
    // Time between wincondition met and the next round starting
    public float gameRestTimer = 3;

    public Image player1Health;
    public Image player2Health;
    //public Text winnerText;
    // Displays each players score
    public Image[] player1RoundUI;
    public Image[] player2RoundUI;

    private bool gameFinished = false;


    // Update is called once per frame
    void Update()
    {
        // Test for winner
        if (player1Health.fillAmount <= 0 && gameFinished != true)
        {
            // Player 2 wins
            gameFinished = true;
            ++player2Score;
            ++currentRound;
            //winnerText.text = "Player 2 wins!";
        }
        else if (player2Health.fillAmount <= 0 && gameFinished != true)
        {
            // Player 1 wins
            gameFinished = true;
            ++player1Score;
            ++currentRound;
            //winnerText.text = "Player 1 wins!";
        }


        // Test for max rounds met
        if (currentRound > maxRound)
        {
            // Game finished
            Invoke("GameOver", gameRestTimer);
        }
        else
        {
            // Round reset
            Invoke("ResetRound", gameRestTimer);
        }


        // Test if player has reached max score
        if (player1Score >= maxPlayerWins)
        {
            // Game finished
            Invoke("GameOver", gameRestTimer);
        }
        else if (player2Score >= maxPlayerWins)
        {
            // Game finished
            Invoke("GameOver", gameRestTimer);
        }

        // Displaying each players score
        // Player 1
        for (int i = 0; i < player1RoundUI.Length; i++)
        {
            if (player1Score > i)
            {
                player1RoundUI[i].color = new Color(0, 1, 0);
            }
            else
            {
                player1RoundUI[i].color = new Color(1, 1, 1);
            }
        }

        // Player 2
        for (int i = 0; i < player2RoundUI.Length; i++)
        {
            if (player2Score > i)
            {
                player2RoundUI[i].color = new Color(0, 1, 0);
            }
            else
            {
                player2RoundUI[i].color = new Color(1, 1, 1);
            }
        }
    }

   private void ResetRound()
    {
        // Restarts the scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("NAME OF GAMEOVER SCENE"); // Gameover scene could just be the menu again;
    }
}
