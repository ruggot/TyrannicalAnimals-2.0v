using System;
using UnityEngine;
using UnityEngine.UI;

[Obsolete("DamageCalc.cs is deprecated, please use Player.cs instead.", true)]
public class DamageCalc : MonoBehaviour
{
    // Setting player starting Health Points
    private float Player1HP = 1f;
    private float Player2HP = 1f;
    // Defining players health bars
    public Image Player1HPbar;
    public Image Player2HPbar;
    // Setting player starting Fury
    private float Player1Fury = 0f;
    private float Player2Fury = 0f;
    // Defining players fury bars
    public Image Player1Furybar;
    public Image Player2Furybar;
    // Variables for P1 abilities for flexible values
    private float P1Light;
    private float P1Heavy;
    private float P1Special;
    private float P1LightFuryGen;
    private float P1HeavyFuryGen;
    // Variables for P2 Abilities for flexible values
    private float P2Light;
    private float P2Heavy;
    private float P2Special;
    private float P2LightFuryGen;
    private float P2HeavyFuryGen;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    { // Checking for any trigger collision with other objects


        Debug.Log("Player Hit");
        if (other.gameObject.tag == "Player")
        { // On the event of ANY player collision
            Debug.Log("Player Hit A Player");
            //Player Light attack damage
            if (other.gameObject.name == "Chicken_P2" && gameObject.name == "LightHit")
            { // If the enemy is Player 2 and the collider that hit was Light, Deal player 1's Light damage
                Player2HP -= P1Light;
                Player2HPbar.fillAmount = Player2HP;
                Player1Fury += P1LightFuryGen;
                Player1Furybar.fillAmount = Player1Fury;
            }
            if (other.gameObject.name == "Chicken_P1" && gameObject.name == "P2LightHit")
            {
                Player1HP -= P2Light;
                Player1HPbar.fillAmount = Player1HP;
                Player2Fury += P2LightFuryGen;
                Player2Furybar.fillAmount = Player2Fury;
                Debug.Log("Lightp1");
            }
            // Player Heavy attack damage
            if (other.gameObject.name == "Chicken_P2" && gameObject.name == "P1HeavyHit")
            { // If the enemy is Player 2 and the collider that hit was Heavy, Deal player 1's Heavy damage
                Player2HP -= P1Heavy;
                Player2HPbar.fillAmount = Player2HP;
                Player1Fury += P1HeavyFuryGen;
                Player1Furybar.fillAmount = Player1Fury;
                Debug.Log("Heavyp1");
            }
            if (other.gameObject.name == "Chicken_P1" && gameObject.name == "P2HeavyHit")
            {
                Player1HP -= P2Heavy;
                Player1HPbar.fillAmount = Player1HP;
                Player2Fury += P2HeavyFuryGen;
                Player2Furybar.fillAmount = Player2Fury;
                Debug.Log("Heavyp2?");
            }
            // Player Special attack damage
            if (other.gameObject.name == "Chicken_P2" && gameObject.name == "P1SpecialHit")
            { // If the enemy is Player 2 and the collider that hit was Special, Deal player 1's Special damage.
                Player2HP -= P1Special;
                Player2HPbar.fillAmount = Player2HP;
                Debug.Log("specialp1");
            }
            if (other.gameObject.name == "Chicken_P1" && gameObject.name == "P2SpecialHit")
            {
                Player1HP -= P2Special;
                Player1HPbar.fillAmount = Player1HP;
            }
        }
    }

    public void ChickenP1()
    {
        P1Light = 0.06f;
        P1Heavy = 0.11f;
        P1Special = 0.08f;
        P1LightFuryGen = 0.35f;
        P1HeavyFuryGen = 0.65f;
        Debug.Log("ChickenP1 Selected");
    }
    public void ChickenP2()
    {
        P2Light = 0.06f;
        P2Heavy = 0.11f;
        P2Special = 0.08f;
        P2LightFuryGen = 0.35f;
        P2HeavyFuryGen = 0.65f;
        Debug.Log("ChickenP2 Selected");
    }
    public void LionP1()
    {
        P1Light = 0.08f;
        P1Heavy = 0.13f;
        P1Special = 0.0f;
        P1LightFuryGen = 0.15f;
        P1HeavyFuryGen = 0.25f;
        Debug.Log("LionP1 Selected");
    }
    public void LionP2()
    {
        P2Light = 0.08f;
        P2Heavy = 0.13f;
        P2Special = 0.0f;
        P2LightFuryGen = 0.15f;
        P2HeavyFuryGen = 0.25f;
        Debug.Log("LionP2 Selected");
    }
    public void PenguinP1()
    {
        P1Light = 0.07f;
        P1Heavy = 0.12f;
        P1Special = 0.10f;
        P1LightFuryGen = 0.20f;
        P1HeavyFuryGen = 0.30f;
    }
    public void PenguinP2()
    {
        P2Light = 0.07f;
        P2Heavy = 0.12f;
        P2Special = 0.10f;
        P2LightFuryGen = 0.20f;
        P2HeavyFuryGen = 0.30f;
    }
}
