using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
    /// In-Game GUI
    [SerializeField] private GameObject gui;

    [SerializeField] private GameObject egoP1;
    [SerializeField] private GameObject egoP2;

    private string[] playerGamepad = new string[2] { "360", "360" };

    [SerializeField]protected GameObject[] player;

    public UnityEvent onStartButton;
    public static bool paused = false;
    public static bool inGame = false;

    public static event Action OnBeginLevel;

    private void Awake()
    {
        DataManager.Players = new GameObject[2] { egoP1, egoP2 };
        DataManager.PlayerGamepad = new string[2] { playerGamepad[0], playerGamepad[1] };
        player = new GameObject[2] { DataManager.Players[0], DataManager.Players[1] };
        // Debug.Log("\n\tPlayer 1 EGO: " + DataManager.Players[1] + "\n\tPlayer 2 EGO: " + DataManager.Players[1]);
    }

    private void Start()
    {

        if (mainCamera.activeInHierarchy) mainCamera.SetActive(false);
        if (gui.activeInHierarchy) gui.SetActive(false);

        Cursor.visible = false; // Hides the cursor upon the game opening
    }

    void Update()
    {
        if ((Input.GetButtonDown("J1_Start_" + playerGamepad[0]) || Input.GetButtonDown("J2_Start_" + playerGamepad[1])) && inGame)
        {
            UnityEvent temp = onStartButton;
            if (temp != null)
            {
                temp.Invoke();
            }
        }
    }

    public void Quit() => Application.Quit();                       /// Quit application
    public void Options() => SceneManager.LoadScene("Options");     /// Load Options.unity
    public void Level() => SceneManager.LoadScene("LevelSelect");   /// Load LevelSelect.unity
    public void Menu() => SceneManager.LoadScene("Menu");           /// Load Menu.unity
    public void LevelOne() => SceneManager.LoadScene("Level_1");    /// Load Level_1.unity
    public void HowToPlay() => SceneManager.LoadScene("HowToPlay"); /// Load HowToPlay.unity

    // Access tutorial from menu

    /// End Character Selection and begin the level  
    public void BeginLevel()
    {
        characterSelectPerspective.SetActive(false);
        mainCamera.SetActive(true);
        OnBeginLevel();
        //FinaliseCharacterModels();
        inGame = true;
    }

    //private void FinaliseCharacterModels()
    //{
    //    player[0] = DataManager.Player(0);
    //    player[1] = DataManager.Player(1);
    //    PlayerOf(0).EnemyPlayer = PlayerOf(1);
    //    PlayerOf(1).EnemyPlayer = PlayerOf(0);

    //    Player[] tempList = new Player[2];
    //    //foreach (var pl in player)
    //    //{
    //    //    foreach (var trans in ExclusiveChildrenOf(pl))
    //    //    {
    //    //        Player p = PlayerOf(trans.gameObject);
    //    //        if (p.Fighter == DataManager.PlayerSelection[p.PlayerVal])
    //    //        {
    //    //            trans.gameObject.SetActive(true);
    //    //        }
    //    //    }
    //    //}

    //    int i = 0;
    //    foreach (var ego in gui.GetComponents<Transform>().Where(t => t.name.Substring(0, 3).Equals("EGO")))
    //    {
    //        Player p = player[i].GetComponent<Player>();
    //        PlayerController c = player[i].GetComponent<PlayerController>();
    //        //p.PlayerHpBar = ego.GetChild(2).GetComponent<Image>();
    //        p.PlayerFuryBar = ego.GetChild(5).GetComponent<Image>();
    //        c.LightUI = ego.GetChild(6).GetComponent<Image>();
    //        c.HeavyUI = ego.GetChild(8).GetComponent<Image>();
    //        c.SpecialUI = ego.GetChild(1).GetComponent<Image>();
    //        c.UtilityUI = ego.GetChild(1).GetComponent<Image>();
    //        Debug.Log($"GUI Finalised {++i} time(s)");
    //    }
    //}

    void DisableCam()
    {
        mainCamera.SetActive(false);
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

    public void UpdateP1Fighter(int fighter)
    {
        print(egoP1.transform.childCount);
        DataManager.PlayerSelection[0] = fighter;
        Debug.Log(egoP1.transform.GetChild(fighter-1).gameObject.ToString());
        DataManager.Players[0] = egoP1.transform.GetChild(fighter-1).gameObject;
        print(DataManager.Players[0]);
    }

    public void UpdateP2Fighter(int fighter)
    {
        DataManager.PlayerSelection[1] = fighter;
        Debug.Log(egoP2.transform.GetChild(fighter-1).gameObject.ToString());
        DataManager.Players[1] = egoP2.transform.GetChild(fighter-1).gameObject;
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

    Player PlayerOf(int p)
    {
        return player[p].GetComponent<Player>();
    }

    Player PlayerOf(GameObject p)
    {
        return p.GetComponent<Player>();
    }

    Transform[] ExclusiveChildrenOf(Transform parent)
    {
        return parent.gameObject.GetComponentsInChildren<Transform>().Where(tf => tf.parent.Equals(parent.gameObject)) as Transform[];
    }

    Transform[] ExclusiveChildrenOf(GameObject parent)
    {
        return parent.gameObject.GetComponentsInChildren<Transform>().Where(tf => tf.parent.Equals(parent.gameObject)) as Transform[];
    }

    // Transform[] ExclusiveChildrenOf(GameObject parent)
    // {

    //     var tList = from t in parent.GetComponentsInChildren<Transform>()
    //                 where t.parent.Equals(parent)
    //                 select t;
    //     return tList;
    // }
}
