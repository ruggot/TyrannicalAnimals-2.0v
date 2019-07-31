using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected Player self;
    private Player enemyPlayer;

    [SerializeField] private int fighter;
    private Image playerHpBar;
    private Image playerFuryBar;

    private PlayerController controller;
    private int playerVal;

    private string pLog;
    protected float playerHp;
    protected float playerFury = 0f;

    private float lightDmg;
    private float heavyDmg;
    private float specialDmg;
    private float lightFury;
    private float heavyFury;

    private bool canJump = true;
    private bool canLight = true;
    private bool canHeavy = true;
    private bool canUtility = true;
    private bool canSpecial = true;

    internal int Fighter { get => fighter; set => fighter = value; }
    internal int PlayerVal { get => playerVal; set => playerVal = value; }
    internal Player EnemyPlayer { get => enemyPlayer; set => enemyPlayer = value; }
    public Image PlayerHpBar { get => playerHpBar; set => playerHpBar = value; }
    public Image PlayerFuryBar { get => playerFuryBar; set => playerFuryBar = value; }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        controller = self.GetComponent<PlayerController>();
        playerVal = controller.player;      
        // playerHp = DataManager.Hp[playerVal - 1];
        pLog = "P" + playerVal;
        Debug.Log(pLog + ": enemyplayer.name = " + enemyPlayer.name);

    }

    // Update is called once per frame
    void Update()
    {
        if (!Fighter.Equals(DataManager.PlayerSelection[playerVal - 1]))
        {
            Fighter = DataManager.PlayerSelection[playerVal - 1];
            SetFighter(Fighter);
        }
        UpdateData();
        ReenableHitboxes();
    }

    void SetFighter(int fighter)
    {
        /// 1 = Chicken; 2 = Lion; 3 = Penguin
        switch (fighter)
        {
            case 1:
                {
                    lightDmg = 0.06f;
                    heavyDmg = 0.11f;
                    specialDmg = 0.08f;
                    lightFury = 0.35f;
                    heavyFury = 0.65f;
                    Debug.Log(pLog + "Chicken selected");
                    break;
                }
            case 2:
                {
                    lightDmg = 0.08f;
                    heavyDmg = 0.13f;
                    specialDmg = 0.00f;
                    lightFury = 0.15f;
                    heavyFury = 0.25f;
                    Debug.Log(pLog + ": Lion selected");
                    break;
                }
            case 3:
                {
                    lightDmg = 0.07f;
                    heavyDmg = 0.12f;
                    specialDmg = 0.10f;
                    lightFury = 0.20f;
                    heavyFury = 0.30f;
                    Debug.Log(pLog + ": Penguin selected");
                    break;
                }
            default:
                Debug.Log(pLog + ": Player must be constructed with a fighter value;");
                lightDmg = 0f;
                heavyDmg = 0f;
                specialDmg = 0f;
                lightFury = 0f;
                heavyFury = 0f;
                break;
        }

    }

    private void UpdateData()
    {
        // DataManager.Hp[playerVal - 1] = playerHp;
    }

    public void TakeDamage(string furyType, float dmg)
    {
        playerHp -= dmg;
        PlayerHpBar.fillAmount = (1 / playerHp) * playerHp;
        switch (furyType)
        {
            case "light": BuildFury(lightFury); break;
            case "heavy": BuildFury(heavyFury); break;
            case "special": break;
            default: Debug.Log(pLog + ": TakeDamage(string furyType) invalid"); break;
        }
    }


    private void BuildFury(float fury)
    {
        playerFury += fury;
        playerFuryBar.fillAmount = playerFury;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            Debug.Log(pLog + ": Hitbox triggered.\n\tother:\t\t" + other.name + ", " + other.tag + "\n\tgameObject:\t" + gameObject.name + ", " + gameObject.tag);
            if (other.tag == enemyPlayer.tag)
            {
                Debug.Log(pLog + ": Player hit");
                switch (other.name)
                {
                    case "LightHit":
                        if (canLight)
                        {
                            TakeDamage("light", lightDmg);
                            Debug.Log(pLog + ": Light damage taken");
                            canLight = false;
                        }
                        break;
                    case "HeavyHit":
                        if (canHeavy)
                        {
                            TakeDamage("heavy", heavyDmg);
                            Debug.Log(pLog + ": Heavy damage taken");
                            canHeavy = false;
                        }
                        break;
                    case "SpecialHit":
                        if (canSpecial)
                        {
                            TakeDamage("special", specialDmg);
                            Debug.Log(pLog + ": Special damage taken");
                            canSpecial = false;
                        }
                        break;
                    default:
                        Debug.Log(pLog + ": Not a damage dealing hitbox");
                        break;
                }
            }
        }
    }

    private void ReenableHitboxes()
    {
        if (controller.LastLight < Time.time + 0.2f) { canLight = true; }
        if (controller.LastHeavy < Time.time + 0.2f) { canHeavy = true; }
        if (controller.LastSpecial < Time.time + 0.2f) { canSpecial = true; }
    }
}
