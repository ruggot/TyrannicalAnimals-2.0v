using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum AttackType {LightHit,HeavyHit,SpecialHit }

public class PlayerController : MonoBehaviour
{

    public AttackType attackType;

    public float movementSpeed = 3;
    // collider for players
    [SerializeField] protected BoxCollider lightHit;
    [SerializeField] protected BoxCollider heavyHit;
    [SerializeField] protected BoxCollider specialHit;
    [SerializeField] protected BoxCollider utilityHit;
    [SerializeField] protected BoxCollider hurtBox;
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

    [SerializeField] internal int player;
    
    [SerializeField] protected string gPad;
    [SerializeField] private Image lightUI;
    [SerializeField] private Image heavyUI;
    [SerializeField] private Image utilityUI;
    [SerializeField] private Image specialUI;

    protected Animator anim;
    protected Rigidbody rb;

    public float LastJump { get => lastJump; set => lastJump = value; }
    public float LastLight { get => lastLight; set => lastLight = value; }
    public float LastHeavy { get => lastHeavy; set => lastHeavy = value; }
    public float LastUtility { get => lastUtility; set => lastUtility = value; }
    public float LastSpecial { get => lastSpecial; set => lastSpecial = value; }
    public Image LightUI { get => lightUI; set => lightUI = value; }
    public Image HeavyUI { get => heavyUI; set => heavyUI = value; }
    public Image UtilityUI { get => utilityUI; set => utilityUI = value; }
    public Image SpecialUI { get => specialUI; set => specialUI = value; }

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
                lightHit.enabled = true;
                canLight = false;
                attackType = AttackType.LightHit;
            }

            if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy)
            {
                anim.SetTrigger("Heavy");
                lastHeavy = Time.time;
                heavyHit.enabled = true;
                canHeavy = false;
                attackType = AttackType.HeavyHit;
            }

            /// Utility attack
            // NOTE  Uncomment when utility added
            if (Input.GetAxisRaw("J" + player + "_Mobility_" + gPad) > 0.3 && Time.time > utilityCool && canUtility)
            {
                anim.SetTrigger("Mobility");
                lastUtility = Time.time;
                canUtility = false;
            }

            if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial)
            {
                anim.SetTrigger("Special");
                lastSpecial = Time.time;
                //specialHit.enabled= true;
                canSpecial = false;
                attackType = AttackType.SpecialHit;
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
            if (canUtility == false)
            {
                utilityUI.fillAmount += 1.0f / utilityCool * Time.deltaTime;
                if (utilityUI.fillAmount >= 1) { utilityUI.fillAmount = 0; canUtility = true; }
            }

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
        if (Time.time >= lastLight + lightCool && lightHit != null) { lightHit.enabled = false; canLight = true; }
        if (Time.time >= lastHeavy + heavyCool && heavyHit != null) { heavyHit.enabled = false; canHeavy = true; }
        if (Time.time >= lastSpecial + specialCool && specialHit != null) { specialHit.enabled = false; canSpecial = true; }
        if (Time.time >= lastUtility + utilityCool && utilityHit != null) { utilityHit.enabled = false; canUtility = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

}
