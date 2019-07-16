using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// I have formatted the methods in this script with expression bodies, as this implements them with a read-only property.
// This also formats them in a very concise and readable form.

public class SceneScript : MonoBehaviour // English -- spelling mistake: SceneScritp -> SceneScript
{

    [SerializeField] private GameObject characterSelectPerspective;
    // Disables Level Select
    [SerializeField] private GameObject mainCamera;
    // Enables Dynamic Camera
    [SerializeField] private string primaryGampadType;

    public string PrimaryGampadType { get => primaryGampadType; set => primaryGampadType = value; }

    public void Quit() => Application.Quit();

    public void Options() => SceneManager.LoadScene("Options"); // English -- The plural "options" is usually used for this

    public void Level() => SceneManager.LoadScene("LevelSelect"); // Convention -- Pascal case (see below) 
    // Consistency/convention -- The rest of the methods were named for their destination (e.g. Options() instead of GoToOptions()), this one should be as well. This also keeps the name appropriately descriptive if you want to use the method from elsewhere

    public void Menu() => SceneManager.LoadScene("Menu"); // English -- Just a spelling mistake: meny -> menu 
    // Convention -- Methods should be named in Pascal case i.e. CapitaliseEveryWord()

    public void LevelOne() => SceneManager.LoadScene("Level_1"); // Convention -- Avoid using spaces when naming scenes, objects, etc.; they can cause issues. Using integers instead of words will keep the files in order no matter how many there are 
    // Convention -- Pascal case

    public void BeginLevel()
    {
        characterSelectPerspective.SetActive(false);
        mainCamera.SetActive(true);
    }
    // Begins the level and ends Character Select

}
