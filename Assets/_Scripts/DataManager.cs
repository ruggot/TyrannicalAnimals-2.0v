using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static int killsP1, killsP2, deathsP1, deathsP2, round;
    // HEAD:Assets/My Scripts/CharacterManager.cs

    //  saves what the player selected as an integer
    private static int[] playerSelection = new int[2] { 1, 1 };
    private static float[] hp = new float[2] { 10f, 10f };
    private static GameObject[] players;
    private static string[] playerGamepad;

    public static int KillsP1 { get; set; }
    public static int KillsP2 { get; set; }
    public static int DeathsP1 { get; set; }
    public static int DeathsP2 { get; set; }
    public static int Round { get; set; }

    /// 1 = Chicken, 2 = Lion, 3 = Penguin
    public static int[] PlayerSelection { get; set; }
    public static string[] PlayerGamepad { get; set; }
    public static float[] Hp { get; set; }
    public static GameObject[] Players { get; set; }

    public static GameObject Player(int p) => Players[p].GetComponent<Transform>().GetChild(playerSelection[p]).gameObject;
}
