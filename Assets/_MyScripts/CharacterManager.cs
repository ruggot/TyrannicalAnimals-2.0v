using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // players character
    public GameObject chicken;
    public GameObject shadowChicken;
<<<<<<< HEAD:Assets/My Scripts/CharacterManager.cs
    // string that saves what the player selected as a string
=======
    public string playerOne;
>>>>>>> 03f1cc31fd09cf714928862f8761909fb1eccccd:Assets/_MyScripts/CharacterManager.cs


    private int playerSelection;

    public void SpawnChicken ()
    {
        shadowChicken.SetActive(false);
        chicken.SetActive(true);
    }

    public void SpawnShadowChicken()
    {
        chicken.SetActive(false);
        shadowChicken.SetActive(true);
    }
}
