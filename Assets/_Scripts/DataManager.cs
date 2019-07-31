using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static int killsP1, killsP2, deathsP1, deathsP2, round;
    // HEAD:Assets/My Scripts/CharacterManager.cs

    // int that saves what the player selected as a string
    private static int[] playerSelection = new int[2];
    private static float[] hp = new float[2] { 0f, 0f };
    private static GameObject[] players = new GameObject[2];
    private static string[] playerGamepad = new string[2] { "360", "360" };

    public static int KillsP1 { get => killsP1; set => killsP1 = value; }
    public static int KillsP2 { get => killsP2; set => killsP2 = value; }
    public static int DeathsP1 { get => deathsP1; set => deathsP1 = value; }
    public static int DeathsP2 { get => deathsP2; set => deathsP2 = value; }
    public static int Round { get => round; set => round = value; }

    /// 1 = Chicken, 2 = Lion, 3 = Penguin
    public static int[] PlayerSelection { get => playerSelection; set => playerSelection = value; }
    public static string[] PlayerGamepad { get => playerGamepad; set => playerGamepad = value; }
    public static float[] Hp { get => hp; set => hp = value; }
    public static GameObject[] Players { get => players; set => players = value; }
}
