using System.Collections;
using System.Collections.Generic;
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
    /// Gamepad currently in use

    [SerializeField] public GameObject egoP1;
    [SerializeField] public GameObject egoP2;

    private string[] playerGamepad = new string[2];

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject[] players;

    public UnityEvent onStartButton;
    public static bool paused = false;
    public static bool inGame = false;

    public string[] PlayerGamepad { get => playerGamepad; set => playerGamepad = value; }

    private void Awake()
    {
        // ensure correct objects are disabled/enabled
        players = new GameObject[2] { playerOne, playerTwo };
        if (mainCamera.activeInHierarchy) mainCamera.SetActive(false);
        if (GameObject.Find("GUI").activeInHierarchy) GameObject.Find("GUI").SetActive(false);
        if (GameObject.Find("EGO DayObject").activeInHierarchy == false) GameObject.Find("EGO DayObject").SetActive(true);
        if (GameObject.Find("EGO Character_Select").activeInHierarchy == false) GameObject.Find("EGO Character_Select").SetActive(true);
        Cursor.visible = false; // Hides the cursor upon the game opening
    }

    private void Start()
    {
        playerGamepad[0] = DataManager.PlayerGamepad[0];
        playerGamepad[1] = DataManager.PlayerGamepad[1];
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
        playerOne = DataManager.Players[0];
        playerTwo = DataManager.Players[1];
    }

    /// Quit application
    public void Quit() => Application.Quit();

    /// Load Options.unity
    public void Options() => SceneManager.LoadScene("Options");

    /// Load LevelSelect.unity
    public void Level() => SceneManager.LoadScene("LevelSelect");

    /// Load Menu.unity
    public void Menu() => SceneManager.LoadScene("Menu");

    /// Load Level_1.unity
    public void LevelOne() => SceneManager.LoadScene("Level_1");

    /// Load HowToPlay.unity
    public void HowToPlay() => SceneManager.LoadScene("HowToPlay");
    // Access tutorial from menu

    /// End Character Selection and begin the level  
    public void BeginLevel()
    {
        mainCamera.SetActive(true);
        FinaliseCharacterModels();
        characterSelectPerspective.SetActive(false);
        inGame = true;
    }

    private void FinaliseCharacterModels()
    {
        int i = 0;
        Player[] tempList = new Player[1];
        foreach (var pl in players)
        {
            Debug.Log(pl.GetComponents<GameObject>().ToString());

            foreach (var model in pl.GetComponents<GameObject>())
            {
                Player p = model.GetComponent<Player>();
                if (p.Fighter == DataManager.PlayerSelection[p.PlayerVal])
                {
                    model.SetActive(true);
                    tempList[i] = p;
                    i++;
                }
            }
        }

        tempList[0].EnemyPlayer = tempList[1];
        tempList[1].EnemyPlayer = tempList[0];
        i = 0;
        foreach (var ego in GameObject.Find("GUI").GetComponents<GameObject>())
        {
            Player p = players[i].GetComponent<Player>();
            PlayerController c = players[i].GetComponent<PlayerController>();
            p.PlayerHpBar = ego.GetComponentsInChildren<GameObject>()[2].GetComponent<Image>();
            p.PlayerFuryBar = ego.GetComponentsInChildren<GameObject>()[5].GetComponent<Image>();
            c.LightUI = ego.GetComponentsInChildren<GameObject>()[6].GetComponent<Image>();
            c.HeavyUI = ego.GetComponentsInChildren<GameObject>()[8].GetComponent<Image>();
            c.UtilityUI = ego.GetComponentsInChildren<GameObject>()[10].GetComponent<Image>();
            c.SpecialUI = ego.GetComponentsInChildren<GameObject>()[12].GetComponent<Image>();
            i++;
        }
    }

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
        DataManager.PlayerSelection[0] = fighter;
        Debug.Log(egoP1.gameObject.GetComponentsInChildren<GameObject>(true)[fighter-1].ToString());
        DataManager.Players[0] = egoP1.gameObject.GetComponentsInChildren<GameObject>(true)[fighter-1];
        mainCamera.gameObject.GetComponent<CameraController>().Player1 = playerOne; 

    }

    public void UpdateP2Fighter(int fighter)
    {
        DataManager.PlayerSelection[1] = fighter;
        DataManager.Players[1] = egoP2.gameObject.GetComponentsInChildren<GameObject>(true)[fighter-1];
        Debug.Log(egoP2.gameObject.GetComponentsInChildren<GameObject>(true)[fighter-1].ToString());
        mainCamera.gameObject.GetComponent<CameraController>().Player2 = playerTwo; 
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
}
