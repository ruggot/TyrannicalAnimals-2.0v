using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // HEAD:Assets/My Scripts/CharacterManager.cs
    // int that saves what the player selected as a string
    public int player1Selection = 0;
    public int player2Selection = 0;

    public int GetPlayer2Selection()
    {
        return player2Selection;
    }

    public void SetPlayer2Selection(int value)
    {
        player2Selection = value;
    }

    public int GetPlayer1Selection()
    {
        return player1Selection;
    }

    public void SetPlayer1Selection(int value)
    {
        player1Selection = value;
    }
}
