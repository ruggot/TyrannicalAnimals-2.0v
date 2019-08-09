using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManagementSystem : MonoBehaviour
{
    // Players current scores
    public static int player1Score;
    public static int player2Score;
    // Current round
    public static int currentRound;
    // max score a player can have
    public int maxPlayerWins = 3;
    // Time between wincondition met and the next round starting
    public float gameRestTimer = 3;
    private float currentGameRestTimer;
    // Used to get player health
    public Image player1Health;
    public Image player2Health;
    // Displays who the winner is
    //public Text winnerText;
    // Displays each players score
    public Image[] player1RoundUI;
    public Image[] player2RoundUI;
    // GUI elements to be disabled or enabled
    public GameObject[] guiDisabledOnWin;
    public GameObject guiEnableOnWin;
    // Player Scripts
    public Player player1Script;
    public Player player2Script;

    public bool gameFinished = false;


    // Hide win text
    private void Start()
    {
        //winnerText.text = "";
        currentGameRestTimer = gameRestTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // Set player 1 script
        SetPlayer(player1Script);
        // Set player 2 script
        SetPlayer(player2Script);

        // Test for winner
        if (gameFinished != true && player1Health.fillAmount <= 0 && player1Health.gameObject.activeSelf == true)
        {
            // Player 2 wins
            gameFinished = true;
            ++player2Score;
            ++currentRound;
            //winnerText.text = "Player 2 wins!";
        }
        else if (gameFinished != true && player2Health.fillAmount <= 0 && player2Health.gameObject.activeSelf == true)
        {
            // Player 1 wins
            gameFinished = true;
            ++player1Score;
            ++currentRound;
            //winnerText.text = "Player 1 wins!";
        }

        // Test if player has reached max score
        if (gameFinished == true)
        {
            if (player1Score >= maxPlayerWins) // Game finished
            {
                // Enable/disable gui objects
                for (int i = 0; i < guiDisabledOnWin.Length; i++)
                {
                    guiDisabledOnWin[i].SetActive(false);
                }
                guiEnableOnWin.SetActive(true);

                // Go to win scene
                Invoke("GameOver", gameRestTimer);
            }
            else if (player2Score >= maxPlayerWins) // Game finished
            {
                // Enable/disable gui objects
                for (int i = 0; i < guiDisabledOnWin.Length; i++)
                {
                    guiDisabledOnWin[i].SetActive(false);
                }
                guiEnableOnWin.SetActive(true);

                // Go to win scene
                Invoke("GameOver", gameRestTimer);
            }
            else // Round finished
            {
                // Round reset
                currentGameRestTimer -= Time.deltaTime;
                if (currentGameRestTimer < 0)
                {
                    ResetRound();
                    currentGameRestTimer = gameRestTimer;
                }

            }
        }

        // Get player scripts

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

    private void SetPlayer(Player player)
    {
        // Set player script
        if (player == null)
        {
            foreach (GameObject i in GameObject.FindGameObjectsWithTag(player.tag))
            {
                if (i.GetComponent<Player>() != null)
                {
                    player = i.GetComponent<Player>();
                }
            }
        }
    }

    private void ResetRound()
    {
        // Restarts the round
        // Invoke player reset methods
        // Hide/show gui
        player1Script.ResetCharacter();
        player2Script.ResetCharacter();
        // player1Health.fillAmount = 1f;
        // player2Health.fillAmount = 1f;
        gameFinished = false;
        print("RoundReset");
    }

    private void GameOver()
    {
        SceneManager.LoadScene("NAME OF GAMEOVER SCENE"); // Gameover scene could just be the menu again
    }
}
