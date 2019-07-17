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

    public string Player2Gampad { get => player2Gamepad; set => player2Gamepad = value; }
    public string Player1Gampad { get => player1Gamepad; set => player1Gamepad = value; }

    private void Start()
    {
        // ensure correct objects are disabled
        mainCamera.SetActive(false);
        GameObject.Find("Chicken_P1").SetActive(false);
        GameObject.Find("Chicken_P2").SetActive(false);
        GameObject.Find("HUD").SetActive(false);
        GameObject.Find("EGO DayObject").SetActive(true);
        GameObject.Find("EGO NightObject").SetActive(false);
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
    // English -- The plural "options" is usually used for this
    /// Load LevelSelect.unity
    public void Level() => SceneManager.LoadScene("LevelSelect");
    // Convention -- Pascal case (see below) 
    // Consistency/convention -- The rest of the methods were named for their destination (e.g. Options() instead of GoToOptions()), this one should be as well. This also keeps the name appropriately descriptive if you want to use the method from elsewhere
    /// Load Menu.unity
    public void Menu() => SceneManager.LoadScene("Menu");
    // English -- Just a spelling mistake: meny -> menu 
    // Convention -- Methods should be named in Pascal case i.e. CapitaliseEveryWord()
    /// Load Level_1.unity
    public void LevelOne() => SceneManager.LoadScene("Level_1");
    // Convention -- Avoid using spaces when naming scenes, objects, etc.; they can cause issues. Using integers instead of words will keep the files in order no matter how many there are 
    // Convention -- Pascal case

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