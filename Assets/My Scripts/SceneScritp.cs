using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScritp : MonoBehaviour
{
    public void Quiting()
    {
        Application.Quit();
    }
    public void Option()
    {
        SceneManager.LoadScene("Option");
    }
    public void Play()
    {
        SceneManager.LoadScene("Level selection");
    }
    public void BackToMeny()
    {
        SceneManager.LoadScene("Meny");
    }
    public void Levelone()
    {
        SceneManager.LoadScene("level 1");
    }
    public void Leveltwo()
    {
        SceneManager.LoadScene("level 2");
    }
}
