using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] protected Player self;
    [SerializeField] protected Player enemyPlayer;

    [SerializeField] protected int fighter;
    [SerializeField] protected Image playerHpBar;
    [SerializeField] protected Image playerFuryBar;

    private string pLog;
    protected float playerHp = 10f;
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


    // constructor
    /// 1 = Chicken; 2 = Lion; 3 = Penguin
    public Player(int fighter)
    {
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
            default: Debug.Log(pLog + ": Player must be constructed with a fighter value;"); break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        self = new Player(fighter);
        pLog = self.name.Remove(0, 6);
        Debug.Log(pLog + ": enemyplayer.name = " + enemyPlayer.name);
        if (CharacterManager.player1Selection == 0)
        {
            // TODO
            // Take player choice from main menu and update player values to match
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReenableHitboxes();
    }


    public void TakeDamage(string furyType, float dmg)
    {
        playerHp -= dmg;
        playerHpBar.fillAmount = 1 / playerHp;
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
        if (self.GetComponent<PlayerController>().LastLight < Time.time + 0.2f) { canLight = true; }
        if (self.GetComponent<PlayerController>().LastHeavy < Time.time + 0.2f) { canHeavy = true; }
        if (self.GetComponent<PlayerController>().LastSpecial < Time.time + 0.2f) { canSpecial = true; }
    }

}
