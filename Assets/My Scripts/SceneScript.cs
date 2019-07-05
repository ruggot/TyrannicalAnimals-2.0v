using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// As the methods are essentially only single expressions, I have changed them to the expression body for efficiency.

public class SceneScript : MonoBehaviour // English -- spelling mistake: SceneScritp -> SceneScript
{
    public void Quit() => Application.Quit();
    public void Options() => SceneManager.LoadScene("Options"); // English -- The plural "options" is usually used for this
    public void Play() => SceneManager.LoadScene("LevelSelect"); // Convention -- Pascal case (see below) 
    public void Menu() // Consistency/convention -- The rest of the methods were named for their destination (e.g. Options() instead of GoToOptions()), this one should be as well. This also keeps the name appropriately descriptive if you want to use the method from elsewhere
=> SceneManager.LoadScene("Menu"); // English -- Just a spelling mistake: meny -> menu
    public void LevelOne() // Convention -- Methods should be named in Pascal case i.e. CapitaliseEveryWord()
=> SceneManager.LoadScene("Level1"); // Convention -- Avoid using spaces when naming scenes, objects, etc.; they can cause issues. Using integers instead of words will keep the files in order no matter how many there are
    public void LevelTwo() // Convention -- Pascal case
=> SceneManager.LoadScene("Level2");
}
