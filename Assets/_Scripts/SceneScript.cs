using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


// I have formatted the methods in this script with expression bodies, as this implements them with a read-only property.
// This also formats them in a very concise and readable form.

public class SceneScript : MonoBehaviour // English -- spelling mistake: SceneScritp -> SceneScript
{
    /// Main camera
    [SerializeField] private GameObject mainCamera;
    /// Character Selection camera
    [SerializeField] private GameObject characterSelectPerspective;
    /// Character Selection object
    [SerializeField] private GameObject characterSelect;
    /// Pause Screen panel
    [SerializeField] private GameObject pausePanel;
    /// Gamepad currently in use
    [SerializeField] private string player1Gamepad;
    [SerializeField] private string player2Gamepad;

    public UnityEvent onStartButton;
    public static bool paused = false;
    public static bool inGame = false;

    public string Player1Gampad { get => player1Gamepad; set => player1Gamepad = value; }
    public string Player2Gampad { get => player2Gamepad; set => player2Gamepad = value; }

    private void Start()
    {
        // ensure correct objects are disabled/enabled
        if (mainCamera.activeInHierarchy) mainCamera.SetActive(false);
        if (GameObject.Find("Chicken_P1").activeInHierarchy) GameObject.Find("Chicken_P1").SetActive(false);
        if (GameObject.Find("Chicken_P2").activeInHierarchy) GameObject.Find("Chicken_P2").SetActive(false);
        if (GameObject.Find("HUD").activeInHierarchy) GameObject.Find("HUD").SetActive(false);
        if (GameObject.Find("EGO NightObject").activeInHierarchy) GameObject.Find("EGO NightObject").SetActive(false);
        if (GameObject.Find("EGO DayObject").activeInHierarchy == false) GameObject.Find("EGO DayObject").SetActive(true);
        if (GameObject.Find("EGO Character_Select").activeInHierarchy == false) GameObject.Find("EGO Character_Select").SetActive(true);
        Cursor.visible = false; //This hides the cursor upon the game opening
    }

    void Update()
    {
        if ((Input.GetButtonDown("J1_Start_" + player1Gamepad) || Input.GetButtonDown("J2_Start_" + player2Gamepad)) && inGame)
        {
            UnityEvent temp = onStartButton;
            if (temp != null)
            {
                temp.Invoke();
            }
        }
    }

    /// Quit application
    public void Quit() => Application.Quit();
    /// Load Options.unity
    public void Options() => SceneManager.LoadScene("Options");

    /// Load LevelSelect.unity
    public void Level() => SceneManager.LoadScene("LevelSelect");

    /// Load Menu.unity
    public void Menu() => SceneManager.LoadScene("Menu");

    /// Load Level_1.unity
    public void LevelOne() => SceneManager.LoadScene("Level_1");

    /// Load HowToPlay.unity
    public void HowToPlay() => SceneManager.LoadScene("HowToPlay");
    // Access tutorial from menu

    /// End Character Select and begin the level  
    public void BeginLevel()
    {
        characterSelectPerspective.SetActive(false);
        mainCamera.SetActive(true);
        inGame = true;
    }

    public void Pause()
    {
        // Pause stuff
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        inGame = false;
        paused = true;
    }

    public void Resume()
    {
        //Resumes game
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        inGame = true;
        paused = false;

    }

    /// Toggle Pause state between <see cref="Pause"/> and <see cref="Unpause"/> 
    public void TogglePause()
    {
        if (paused)
        {
            paused = false;
            Resume();
        }
        else
        {
            paused = true;
            Pause();
        }
    }
}
