using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 3;
    // collider for players
    [SerializeField] protected BoxCollider lightHit;
    [SerializeField] protected BoxCollider heavyHit;
    [SerializeField] protected BoxCollider specialHit;
    [SerializeField] protected BoxCollider utilityHit;
    [SerializeField] protected BoxCollider bodyCollider;

    [SerializeField] protected float jumpForce;

    // Cooldown for player ablitys
    private float jumpCool = 1f;
    private float lightCool = 0.3f;
    private float heavyCool = 1.2f;
    private float utilityCool = 1.2f;
    private float specialCool = 1.2f;
    // how long it has been since the player push that button
    private float lastJump = 0f;
    private float lastLight = 0f;
    private float lastHeavy = 0f;
    private float lastUtility = 0f;
    private float lastSpecial = 0f;
    // bool to check if the player can do the input
    private bool canJump = true;
    private bool canLight = true;
    private bool canHeavy = true;
    private bool canUtility = true;
    private bool canSpecial = true;

    [SerializeField] protected string player;
    [SerializeField] protected string gPad;
    [SerializeField] protected Image lightUI;
    [SerializeField] protected Image heavyUI;
    [SerializeField] protected Image utilityUI;
    [SerializeField] protected Image specialUI;

    protected Animator anim;
    protected Rigidbody rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ControlPlayer();
    }


    protected void ControlPlayer()
    {
        if (SceneScript.inGame)
        {
            float moveHorizontal = Input.GetAxisRaw("J" + player + "_Horizontal_" + gPad);
            float moveVertical = Input.GetAxisRaw("J" + player + "_Vertical_" + gPad);

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
                anim.SetInteger("Walk", 1);
            }
            else anim.SetInteger("Walk", 0);

            rb.AddForce(movement * movementSpeed * Time.deltaTime * 10, ForceMode.VelocityChange);

            if (Input.GetButtonDown("J" + player + "_Jump_" + gPad) && Time.time > jumpCool && canJump)
            {
                rb.AddForce(0, jumpForce, 0);
                lastJump = Time.time + jumpCool;
                anim.SetTrigger("Jump");
                canJump = false;
            }

            if (Input.GetButtonDown("J" + player + "_Light_" + gPad) && Time.time > lightCool && canLight)
            {
                anim.SetTrigger("Peck");
                lastLight = Time.time + lightCool;
                lightHit.enabled = true;
                canLight = false;
            }

            if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy)
            {
                anim.SetTrigger("Heavy");
                lastHeavy = Time.time + heavyCool;
                heavyHit.enabled = true;
                canHeavy = false;
            }

            /// Utility attack
            // NOTE  Uncomment when utility added
            //            if (Input.GetButtonDown("J" + player + "_Utility_" + gPad) && Time.time > utilityDelay && canUtility)
            //            {
            //                anim.SetTrigger("Utility");
            //                sinceUtility = Time.time + utilityDelay;
            //            }

            if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial)
            {
                anim.SetTrigger("Special");
                lastSpecial = Time.time + specialCool;
                //specialHit.enabled = true;
                canSpecial = false;
            }

            // starts timer for light attack
            if (canLight == false)
            {
                lightUI.fillAmount += 1f / lightCool * Time.deltaTime;
            }

            if (canHeavy == false)
            {
                heavyUI.fillAmount += 1f / heavyCool * Time.deltaTime;
            }

            // Actived when utility attack is working
            //if (canUtility == false)
            //{
            //    utilityAttack.fillAmount += 1.0f / utilityCool * Time.deltaTime;
            //}

            if (canSpecial == false)
            {
                specialUI.fillAmount += 1f / specialCool * Time.deltaTime;
            }

            if (lightUI.fillAmount >= 1)
            {
                lightUI.fillAmount = 0;
                canLight = true;
            }

            if (heavyUI.fillAmount >= 1)
            {
                heavyUI.fillAmount = 0;
                canHeavy = true;
            }

            // Actived when utility attack is working
            //if (utilityAttack.fillAmount >= 1)
            //{
            //    utilityAttack.fillAmount = 0;
            //    canUtility = true;
            //}

            if (specialUI.fillAmount >= 1)
            {
                specialUI.fillAmount = 0;
                canSpecial = true;
            }

            //if (utilityAttack.fillAmount >= 1)
            //{
            //    utilityAttack.fillAmount = 0;
            //    canUtility = true;
            //}

            //if (specialAttack.fillAmount >= 1)
            //{
            //    specialAttack.fillAmount = 0;
            //    canSpecial = true;
            //}

            FastFall();
            ResetAbilities();
        }
    }

    private void FastFall()
    {
        if (rb.velocity.y < -0.01) { rb.AddForce(Vector3.down * 30, ForceMode.Acceleration); }
        //        else { rb.mass = 1f; }
    }

    private void ResetAbilities()
    {
        if (Time.time >= lastLight + lightCool) { lightHit.enabled = false; canLight = true; }
        if (Time.time >= lastHeavy + heavyCool) { heavyHit.enabled = false; canHeavy = true; }
        if (Time.time >= lastUtility + utilityCool) { utilityHit.enabled = false; canUtility = true; }
        if (Time.time >= lastSpecial + specialCool) { specialHit.enabled = false; canSpecial = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    private void CooldownForSkillOne()
    {
        // FIXME skillOne.fillAmount += 1 * Time.deltaTime;
    }
}