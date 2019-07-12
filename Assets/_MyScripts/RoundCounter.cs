using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    // countdown and roundtimmer
    public float roundTimer = 400f;
    public float countDowner = 3f;    // points for the players
    private int playerOnePoints;
    private int playerTwoPoints;    // place holder health veribals
    public float playerOneHealth;
    private float playerTwoHealth;    // roundcounter array
    public int[] roundCount;    // refrences player healthbar images
    public Image pOneHealthImage;
    public Image pTwoHealthImage;    // refrences the playermovement
    private PlayerController chickenMovement; void Start() {
        // sets player points to 0
        playerOnePoints = 0;
        playerTwoPoints = 0;        // sets player health to 1
        playerOneHealth = 1f;
        playerTwoHealth = 1f;        // refrence the playermovement script
        chickenMovement = (PlayerController)FindObjectOfType(typeof(PlayerController));        // disables the movement script on the playes
        chickenMovement.enabled = false;
    }    // Update is called once per frame
    void Update() {
        pOneHealthImage.fillAmount = playerOneHealth;
        pTwoHealthImage.fillAmount = playerTwoHealth; countDowner -= Time.deltaTime; if (countDowner < 0) {
            //count down timer for start of rounds
            roundTimer -= Time.deltaTime;
            chickenMovement.enabled = true;
        }
        if (playerOneHealth <= 0) {
            chickenMovement.enabled = false;
            playerOnePoints++;
            SceneManager.LoadScene("level_1");
        }
        if (playerTwoHealth <= 0) {
            chickenMovement.enabled = false;
            playerTwoPoints++;
            SceneManager.LoadScene("Level_1");
        }
        if (playerOnePoints <= 3) {
            WinGame();
            // add UI for this that says that player won this round
        }
        if (playerTwoPoints <= 3) {
            WinGame();
            // add UI for this that says that player won this round
        }
        void WinGame() {
            chickenMovement.enabled = true;
        }
    }
}
