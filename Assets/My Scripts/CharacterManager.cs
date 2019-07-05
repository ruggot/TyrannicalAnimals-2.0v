using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // players character
    public GameObject chicken;
    public GameObject shadowChicken;
    public string playerOne;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    public void Play()
    {

    }
}
