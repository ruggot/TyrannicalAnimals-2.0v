using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // HEAD:Assets/My Scripts/CharacterManager.cs
    // int that saves what the player selected as a string
    public static int player1Selection = 0;
    public static int player2Selection = 1; 
    public static string player1Gamepad = "360";
    public static string player2Gamepad = "360";

    // P1 getter + setter
    public int GetPlayer1Selection() => player1Selection;
    public void SetPlayer1Selection(int value) => player1Selection = value;

    // P2 getter + setter
    public int GetPlayer2Selection() => player2Selection;
    public void SetPlayer2Selection(int value) => player2Selection = value;

}