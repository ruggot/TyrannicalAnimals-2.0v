using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected Player self;
    protected Player enemyPlayer;

    [SerializeField] private int fighter;
    [SerializeField] private Image playerHpBar;
    [SerializeField] private Image playerFuryBar;

    private PlayerController controller;
    private int playerVal;

    private string pLog;
    protected float playerHp = 1;
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

    internal int Fighter { get; set; }
    internal int PlayerVal { get; set; }
    internal Player EnemyPlayer { get; set; }
    public Image PlayerHpBar { get; set; }
    public Image PlayerFuryBar { get; set; }

    private void Start()
    {
        foreach (var item in FindObjectsOfType<Player>())
        {
            if (item != this)
            {
                enemyPlayer = item;
            }
        }
        Debug.Log("" + enemyPlayer);
    }

    private void Awake()
    {
        playerVal--;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        controller = self.GetComponent<PlayerController>();
        playerVal = controller.player;
        // playerHp = DataManager.Hp[playerVal - 1];
        pLog = $"P{playerVal}";
        //Debug.Log($"{pLog}: enemyplayer.name = {enemyPlayer.name}");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Fighter.Equals(DataManager.PlayerSelection[playerVal]))
        {
            Fighter = DataManager.PlayerSelection[playerVal];
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
        print(dmg);
        print(playerFury);
        playerHp -= dmg;
        PlayerHpBar.fillAmount = (1 / playerHp) * playerHp;
        
        switch (furyType)
        {
            case "light": BuildFury(lightFury); break;
            case "heavy": BuildFury(heavyFury); break;
            case "special": break;
            default: Debug.Log($"{pLog}: TakeDamage(string furyType) invalid"); break;
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
            Debug.Log($"{pLog}: Hitbox triggered.\n\tother:\t\t" + other.name + ", " + other.tag + "\n\tgameObject:\t" + gameObject.name + ", " + gameObject.tag);
            if (other.tag == enemyPlayer.tag)
            {
                Debug.Log($"{pLog}: Player hit");
                switch (controller.attackType)
                {
                    case AttackType.LightHit:
                        if (canLight)
                        {
                            TakeDamage("light", lightDmg);
                            Debug.Log($"{pLog}: Light damage taken");
                            canLight = false;
                        }
                        break;
                    case AttackType.HeavyHit:
                        if (canHeavy)
                        {
                            TakeDamage("heavy", heavyDmg);
                            Debug.Log($"{pLog}: Heavy damage taken");
                            canHeavy = false;
                        }
                        break;
                    case AttackType.SpecialHit:
                        if (canSpecial)
                        {
                            TakeDamage("special", specialDmg);
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

    private void ReenableHitboxes()
    {
        if (controller.LastLight < Time.time + 0.2f) { canLight = true; }
        if (controller.LastHeavy < Time.time + 0.2f) { canHeavy = true; }
        if (controller.LastSpecial < Time.time + 0.2f) { canSpecial = true; }
    }
}
