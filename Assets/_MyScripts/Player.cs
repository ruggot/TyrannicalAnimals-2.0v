using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private string pLog = "P1: ";
    [SerializeField] protected Player enemyPlayer;

    [SerializeField] protected Image playerHpBar;
    [SerializeField] protected Image playerFuryBar;

    protected float playerHp = 1f;
    protected float playerFury = 0f;

    private float lightDmg;
    private float heavyDmg;
    private float specialDmg;
    private float lightFury;
    private float heavyFury;

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
                    Debug.Log("Lion selected");
                    break;
                }
            case 3:
                {
                    lightDmg = 0.07f;
                    heavyDmg = 0.12f;
                    specialDmg = 0.10f;
                    lightFury = 0.20f;
                    heavyFury = 0.30f;
                    Debug.Log("Penguin selected");
                    break;
                }
            default: Debug.Log("Player must be constructed with a fighter value;"); break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterManager.player1Selection == 0)
        {
            // TODO
            // Take player choice from main menu and update player values to match
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(string furyType, float dmg)
    {
        playerHp -= dmg;
        playerHpBar.fillAmount = playerHp;
        switch (furyType)
        {
            case "light": TakeFury(lightFury); break;
            case "heavy": TakeFury(heavyFury); break;
            case "special": break;
            default: Debug.Log("TakeDamage(string furyType) invalid"); break;
        }
    }


    private void TakeFury(float fury)
    {
        playerFury += fury;
        playerFuryBar.fillAmount = playerFury;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Hit");
        if (other.gameObject.tag == "Player")
        {
            switch (gameObject.name)
            {
                case "LightHit": enemyPlayer.TakeDamage("light", lightDmg); break;
                case "HeavyHit": enemyPlayer.TakeDamage("heavy", heavyDmg); break;
                case "SpecialHit": enemyPlayer.TakeDamage("special", specialDmg); break;
                default: Debug.Log("Not a damage dealing hitbox"); break;
            }
        }
    }
}
