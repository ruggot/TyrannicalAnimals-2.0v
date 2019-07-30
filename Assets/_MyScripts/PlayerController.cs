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
    /*[SerializeField]*/
    protected BoxCollider specialHit;
    /*[SerializeField]*/
    protected BoxCollider utilityHit; //uncomment [SerializeField] when hitboxes are made
    [SerializeField] protected BoxCollider bodyCollider;
    [SerializeField] protected float jumpForce;

    // Cooldown for player ablitys
    private float jumpCool = 1f;
    private float lightCool = 0.3f;
    private float heavyCool = 1.2f;
    private float utilityCool = 1.2f;
    private float specialCool = 1.2f;
    // how long it has been since the player push that button
    internal float lastJump = 0f;
    internal float lastLight = 0f;
    internal float lastHeavy = 0f;
    internal float lastUtility = 0f;
    internal float lastSpecial = 0f;
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

    public float LastJump { get => lastJump; set => lastJump = value; }
    public float LastLight { get => lastLight; set => lastLight = value; }
    public float LastHeavy { get => lastHeavy; set => lastHeavy = value; }
    public float LastUtility { get => lastUtility; set => lastUtility = value; }
    public float LastSpecial { get => lastSpecial; set => lastSpecial = value; }

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
                lastJump = Time.time;
                anim.SetTrigger("Jump");
                canJump = false;
            }

            if (Input.GetButtonDown("J" + player + "_Light_" + gPad) && Time.time > lightCool && canLight)
            {
                anim.SetTrigger("Peck");
                lastLight = Time.time;
                lightHit.isTrigger = true;
                canLight = false;
            }

            if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy)
            {
                anim.SetTrigger("Heavy");
                lastHeavy = Time.time;
                heavyHit.isTrigger = true;
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
                lastSpecial = Time.time;
                //specialHit.isTrigger= true;
                canSpecial = false;
            }

            // starts timer for light attack
            if (canLight == false)
            {
                lightUI.fillAmount += 1f / lightCool * Time.deltaTime;
                if (lightUI.fillAmount >= 1) { lightUI.fillAmount = 0; canLight = true; }
            }

            if (canHeavy == false)
            {
                heavyUI.fillAmount += 1f / heavyCool * Time.deltaTime;
                if (heavyUI.fillAmount >= 1) { heavyUI.fillAmount = 0; canHeavy = true; }
            }

            if (canSpecial == false)
            {
                specialUI.fillAmount += 1f / specialCool * Time.deltaTime;
                if (specialUI.fillAmount >= 1) { specialUI.fillAmount = 0; canSpecial = true; }
            }

            // Uncomment when utility attack is working
            //if (canUtility == false)
            //{
            //    utilityAttack.fillAmount += 1.0f / utilityCool * Time.deltaTime;
            //    if (utilityAttack.fillAmount >= 1) { utilityAttack.fillAmount = 0; canUtility = true; }
            //}

            FastFall();
            ResetAbilities();
        }
    }

    private void FastFall()
    {
        if (rb.velocity.y < -0.01) { rb.AddForce(Vector3.down * 30, ForceMode.Acceleration); }
    }

    private void ResetAbilities()
    {
        if (Time.time >= lastLight + lightCool) { lightHit.isTrigger = false; canLight = true; }
        if (Time.time >= lastHeavy + heavyCool) { heavyHit.isTrigger = false; canHeavy = true; }
        // if (Time.time >= lastSpecial + specialCool) { specialHit.isTrigger = false; canSpecial = true; }
        // if (Time.time >= lastUtility + utilityCool) { utilityHit.isTrigger= false; canUtility = true; }
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