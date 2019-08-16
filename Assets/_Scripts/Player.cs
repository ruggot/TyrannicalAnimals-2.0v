using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public enum CurrentCharacter { chicken = 1, penguin, lion }

public class Player : MonoBehaviour {
    // what fighter they have chosen
    public CurrentCharacter currentChar = CurrentCharacter.chicken;

    [SerializeField] protected Player self;
    internal Player enemyPlayer;

    // hp and fury images for hpbar and furybar
    public Image playerHpBar;
    [SerializeField] public Image playerFuryBar;

    internal PlayerController controller;
    private int playerVal;

    private string pLog;
    protected float playerHp = 1;
    protected float playerFury = 0f;

    // dmg on attacks
    private float lightDmg;
    private float heavyDmg;
    internal float specialDmg;
    private float lightFury;
    private float heavyFury;
    private Vector3 startingPos;
    private Quaternion startingRotation;

    internal bool lionDmgReduceActive;
    private float dmgReductionFactor = 0.4f;
    private float inputDelay = 0.2f;

    // bools that allowes the player to take actions
    private bool canJump = true;
    private bool canLight = true;
    private bool canHeavy = true;
    private bool canUtility = true;
    private bool canSpecial = true;

    internal int Fighter { get; set; }
    internal int PlayerVal { get; set; }
    //internal Player EnemyPlayer
    //{
    //    get { return enemyPlayer; }
    //    set
    //    {
    //        enemyPlayer = value;
    //        Debug.Log("enemy is set");
    //    }
    //}

    // [SerializeField] protected Image PlayerHpBar { get; set; }
    public Image PlayerFuryBar { get; set; }

    private void Awake() {
        startingPos = transform.position;
        startingRotation = transform.rotation;
    }

    private void Start() => SceneScript.OnBeginLevel += SetPlayers;
    private void OnDestroy() => SceneScript.OnBeginLevel -= SetPlayers;

    // Called when script is enabled within the hierarchy
    void OnEnable() {
        controller = self.GetComponent<PlayerController>();
        playerVal = controller.player;
        pLog = $"P{playerVal}";
        SetFighter();
    }

    // Update is called once per frame
    void Update() {
        UpdateData();
        ReenableAbilities();
    }

    private void SetPlayers() {
        foreach (var item in FindObjectsOfType<Player>()) {
            if (item.gameObject.activeSelf) {
                if (item != this) {
                    enemyPlayer = item;
                }
            }
        }
        Debug.Log($"{pLog}: Enemy player = {enemyPlayer.gameObject.name}");
        SetFighter();
    }

    void SetFighter() {
        /// 1 = Chicken; 2 = Lion; 3 = Penguin
        switch (currentChar) {
            case CurrentCharacter.chicken:
                lightDmg = 0.06f;
                heavyDmg = 0.11f;
                specialDmg = 0.08f;
                lightFury = 0.35f;
                heavyFury = 0.60f;
                Debug.Log($"{pLog}Chicken selected");
                break;
            case CurrentCharacter.lion:
                lightDmg = 0.08f;
                heavyDmg = 0.14f;
                specialDmg = 0.00f;
                lightFury = 0.20f;
                heavyFury = 0.25f;
                Debug.Log($"{pLog}: Lion selected");
                break;
            case CurrentCharacter.penguin:
                lightDmg = 0.07f;
                heavyDmg = 0.12f;
                specialDmg = 0.10f;
                lightFury = 0.25f;
                heavyFury = 0.35f;
                Debug.Log($"{pLog}: Penguin selected");
                break;
            default:
                Debug.Log($"{pLog}: Player must be constructed with a fighter value;");
                lightDmg = 0f;
                heavyDmg = 0f;
                specialDmg = 0f;
                lightFury = 0f;
                heavyFury = 0f;
                break;
        }
    }

    private void UpdateData() => playerHpBar.fillAmount = playerHp;

    public void TakeDamage(string furyType, float dmg) {
        //print(dmg);
        //print(playerFury);
        if (currentChar == CurrentCharacter.lion && lionDmgReduceActive) {
            playerHp -= dmg * dmgReductionFactor;
        }
        else {
            playerHp -= dmg;
        }
        //playerHpBar.fillAmount = (1 / playerHp) * playerHp;

        switch (furyType) {
            case "light":
                BuildFury(lightFury);
                break;
            case "heavy":
                BuildFury(heavyFury);
                break;
            case "special":
                break;
            default:
                Debug.Log($"{pLog}: TakeDamage(string furyType) invalid");
                break;
        }
    }

    private void BuildFury(float fury) => playerFuryBar.fillAmount = playerFury += fury;

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{pLog}: controller.hurtBox = {controller.hurtBox}\ngameobject = {gameObject}\ngameobject.name = {gameObject.name}\nother.name = {other.name}");
        if (other.isTrigger && gameObject == controller.hurtBox) {
            Debug.Log($"{pLog}: Hitbox triggered.\n\tother:\t\t{other.name}, {other.tag}\n\tgameObject:\t{gameObject.name}, {gameObject.tag}");
            if (other.tag == enemyPlayer.tag) {
                Debug.Log($"{pLog}: Player hit");
                switch (controller.attackType) {
                    case AttackType.LightHit:
                        if (canLight) {
                            enemyPlayer.TakeDamage("light", lightDmg);
                            Debug.Log($"{pLog}: Light damage taken");
                            canLight = false;
                        }
                        break;
                    case AttackType.HeavyHit:
                        if (canHeavy && other.gameObject.CompareTag("HeavyHit")) {
                            enemyPlayer.TakeDamage("heavy", heavyDmg);
                            Debug.Log($"{pLog}: Heavy damage taken");
                            canHeavy = false;
                        }
                        break;
                    case AttackType.SpecialHit:
                        if (canSpecial) {
                            enemyPlayer.TakeDamage("special", specialDmg);
                            Debug.Log($"{pLog}: Special damage taken");
                            canSpecial = false;
                        }
                        break;
                    default:
                        Debug.Log($"{pLog}: Not a damage dealing hitbox");
                        Debug.Log("" + other.tag);
                        break;
                }
            }
        }
    }

    private void ReenableAbilities() {
        if (controller.LastLight < Time.time + inputDelay) { canLight = true; }
        if (controller.LastHeavy < Time.time + inputDelay) { canHeavy = true; }
        if (controller.LastSpecial < Time.time + inputDelay) { canSpecial = true; }
    }

    public void ResetCharacter() {
        playerHp = 1f;
        playerFury = 0f;
        transform.position = startingPos;
        transform.rotation = startingRotation;
    }

    public void ChickenOnClick() => currentChar = CurrentCharacter.chicken;
    public void LionOnClick() => currentChar = CurrentCharacter.lion;
    public void PenguinOnClick() => currentChar = CurrentCharacter.penguin;
}
