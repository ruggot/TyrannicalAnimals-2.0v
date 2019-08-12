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
    public float gameResetDelay = 3;
    private float currentResetTimer;
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
    private Player player1Script;
    private Player player2Script;

    public bool gameFinished = false;


    // Hide win text
    private void Start()
    {
        //winnerText.text = "";
        currentResetTimer = gameResetDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Set player 1 script
        if (player1Script == null) SetPlayer(player1Script, "Player 1");
        // Set player 2 script
        if (player2Script == null) SetPlayer(player2Script, "Player 2");

        // Test for winner

        // Test if player has reached max score
        if (!gameFinished)
        {
            if (player1Health.fillAmount <= 0 && player1Health.gameObject.activeSelf)
            {
                // Player 2 wins
                gameFinished = true;
                ++player2Score;
                ++currentRound;
                //winnerText.text = "Player 2 wins!";
            }
            else if (player2Health.fillAmount <= 0 && player2Health.gameObject.activeSelf)
            {
                // Player 1 wins
                gameFinished = true;
                ++player1Score;
                ++currentRound;
                //winnerText.text = "Player 1 wins!";
            }
        }
        else
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
                Invoke("GameOver", gameResetDelay);
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
                Invoke("GameOver", gameResetDelay);
            }
            else // Round finished
            {
                // Round reset
                currentResetTimer -= Time.deltaTime;
                if (currentResetTimer < 0)
                {
                    ResetRound();
                    currentResetTimer = gameResetDelay;
                }
            }
        }

        // Get player scripts

        // Displaying each players score
        // Player 1
        for (int i = 0; i < player1RoundUI.Length; i++)
        {
            if (player1Score > i) player1RoundUI[i].color = new Color(0, 1, 0);
            else player1RoundUI[i].color = new Color(1, 1, 1);
        }

        // Player 2
        for (int i = 0; i < player2RoundUI.Length; i++)
        {
            if (player2Score > i) player2RoundUI[i].color = new Color(0, 1, 0);
            else player2RoundUI[i].color = new Color(1, 1, 1);
        }
    }

    public void EnsurePlayers()
    {
        SetPlayer(player1Script, "Player 1");
        SetPlayer(player2Script, "Player 2");
    }

    private void SetPlayer(Player player, string tag)
    {
        // Set player script if player script null
        foreach (GameObject i in GameObject.FindGameObjectsWithTag(tag))
        {
            if (i.GetComponent<Player>() != null) player = i.GetComponent<Player>();
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