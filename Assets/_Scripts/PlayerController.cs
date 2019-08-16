using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum AttackType { LightHit, HeavyHit, SpecialHit } // Ready = no attack

public class PlayerController : MonoBehaviour
{

    public AttackType attackType;

    public float movementSpeed = 3;
    // collider for players
    [SerializeField] internal BoxCollider lightHit;
    [SerializeField] protected BoxCollider heavyHit;
    [SerializeField] protected BoxCollider specialHit;
    [SerializeField] protected BoxCollider utilityHit;
    [SerializeField] internal BoxCollider hurtBox;
    [SerializeField] protected float jumpForce;
    [SerializeField] GameObject egg;

    private Player player_script;
    // Cooldown for player ablitys
    private float jumpCool = 1f;
    public float lightCool = 0.3f;
    private float heavyCool = 1.2f;
    private float utilityCool = 1.2f;
    private float specialCool = 1.2f;
    internal float timerBetweenAttack = 0.7f;
    internal float timeForSpeedUp = 0.0f;
    protected float timeForStunned;

    float moveHorizontal;
    float moveVertical;
    // how long it has been since the player push that button
    internal float lastJump = 0f;
    internal float lastLight = 0f;
    internal float lastHeavy = 0f;
    internal float lastUtility = 0f;
    internal float lastSpecial = 0f;
    float dashSpeed = 2.5f;
    float ultSpeed = 4f;
    float currentDashSpeed = 1;
    float ultTimer = 0;
    float fury;

    // bool to check if the player can do the input
    private bool canJump = true;
    internal bool canLight = true;
    private bool canHeavy = true;
    private bool canUtility = true;
    private bool canSpecial = true;
    private bool isDashing = false;
    private bool isUlting = false;

    [SerializeField] private bool stunned = false;

    [SerializeField] internal int player;

    [SerializeField] public string gPad;
    [SerializeField] private Image lightUI;
    [SerializeField] private Image heavyUI;
    [SerializeField] private Image utilityUI;
    [SerializeField] private Image specialUI;

	[SerializeField] private AudioSource lightChicken;
	[SerializeField] private AudioSource heavyChicken;
	[SerializeField] private AudioSource utilityChicken;
	[SerializeField] private AudioSource specialChicken;
	[SerializeField] private AudioSource lightLion;
	[SerializeField] private AudioSource heavyLion;
	[SerializeField] private AudioSource utilityLion;
	[SerializeField] private AudioSource specialLion;
	[SerializeField] private AudioSource lightPenguin;
	[SerializeField] private AudioSource heavyPenguin;
	[SerializeField] private AudioSource utilityPenguin;
	[SerializeField] private AudioSource specialPenguin;

	public Vector3 moveDirection;
    public const float maxDashTime = 1.0f;
    //public float dashDistance = 10;
    //public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;

    protected Animator anim;
    protected Rigidbody rb;

    public float LastJump { get => lastJump; set => lastJump = value; }
    public float LastLight { get => lastLight; set => lastLight = value; }
    public float LastHeavy { get => lastHeavy; set => lastHeavy = value; }
    public float LastUtility { get => lastUtility; set => lastUtility = value; }
    public float LastSpecial { get => lastSpecial; set => lastSpecial = value; }

    void Start()
    {
        player_script = GetComponent<Player>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ControlPlayer();
        timerBetweenAttack -= Time.deltaTime;
        timeForSpeedUp -= Time.deltaTime;
        timeForStunned -= Time.deltaTime;

        if (isDashing == true) Dash();
        fury = player_script.playerFuryBar.fillAmount;
    }

    void UnStun()
    {
        stunned = false;
    }
    void LionNotReduceDmg()
    {
        player_script.lionDmgReduceActive = false;
    }
    void Dash()
    {

        // Increase dash speed
        if (currentDashTime > 0)
        {
            currentDashTime -= Time.deltaTime;

            if (isDashing == true)
            {
                currentDashSpeed = ultSpeed;
            }
            else
            {
                currentDashSpeed = dashSpeed;
            }
        }
        else
        {
            currentDashSpeed = 1;
            isDashing = false;
        }
    }

    void PenguinUlt()
    {
        float attackTime = 0.3f;
        while (ultTimer > 0)
        {
            ultTimer -= Time.deltaTime;
            attackTime -= Time.deltaTime;
            if (attackTime < 0)
            {
                // Deal damage
                // Stun
                player_script.enemyPlayer.controller.stunned = true;
                print("Stunned");
            }
        }
        isUlting = false;
        currentDashSpeed = 1;
    }

    protected void ControlPlayer()
    {
        if (stunned == true)
        {
            Invoke("UnStun", 1.5f);
        }

        if (SceneScript.inGame && !stunned)
        {
            if (isDashing != true)
            {
                moveHorizontal = Input.GetAxisRaw("J" + player + "_Horizontal_" + gPad);
                moveVertical = Input.GetAxisRaw("J" + player + "_Vertical_" + gPad);
                print("Can't Change");
            }

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
                anim.SetInteger("Walk", 1);
            }
            else anim.SetInteger("Walk", 0);

            rb.AddForce(movement * movementSpeed * currentDashSpeed * Time.deltaTime * 10, ForceMode.VelocityChange);
            // Movement ability
            if (Input.GetButtonDown("J" + player + "_Jump_" + gPad) && Time.time > jumpCool && canJump)
            {
                rb.AddForce(0, jumpForce, 0);
                lastJump = Time.time;
                anim.SetTrigger("Jump");
                canJump = false;
            }


            // starts timer for attacks
            if (!canLight)
            {
                lightUI.fillAmount += 1f / lightCool * Time.deltaTime;
                if (lightUI.fillAmount >= 1) { lightUI.fillAmount = 0; canLight = true; }
            }

            if (!canHeavy)
            {
                heavyUI.fillAmount += 1f / heavyCool * Time.deltaTime;
                if (heavyUI.fillAmount >= 1) { heavyUI.fillAmount = 0; canHeavy = true; }
            }

            if (!canSpecial)
            {
                specialUI.fillAmount += 1f / specialCool * Time.deltaTime;
                if (specialUI.fillAmount >= 1) { specialUI.fillAmount = 0; canSpecial = true; }
            }

            if (!canUtility)
            {
                utilityUI.fillAmount += 1.0f / utilityCool * Time.deltaTime;
                if (utilityUI.fillAmount >= 1) { utilityUI.fillAmount = 0; canUtility = true; }
            }

            switch (player_script.currentChar)
            {
                case CurrentCharacter.chicken:
                    if (timeForSpeedUp > 0)
                    {
                        movementSpeed = 5f;
                    }
                    else
                    {
                        movementSpeed = 3f;
                    }
                    // Light attack
                    if (Input.GetButtonDown("J" + player + "_Light_" + gPad) && Time.time > lightCool && canLight && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Peck");
                        lastLight = Time.time;
                        lightHit.enabled = true;
                        canLight = false;
                        attackType = AttackType.LightHit;
                        timerBetweenAttack = 0.7f;
						lightChicken.Play();
                    }
                    // Heavy attack
                    if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Heavy");
                        lastHeavy = Time.time;
                        heavyHit.enabled = true;
                        canHeavy = false;
                        attackType = AttackType.HeavyHit;
                        timerBetweenAttack = 0.8f;
						heavyChicken.Play();
                    }
                    // Utility attack
                    if (Input.GetAxisRaw("J" + player + "_Mobility_" + gPad) > 0.3 && Time.time > utilityCool && canUtility && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Mobility");
                        lastUtility = Time.time;
                        canUtility = false;
                        timeForSpeedUp = 1f;
                        timerBetweenAttack = 0.7f;
						utilityChicken.Play();
                    }
                    // Special attack
                    if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial && 0 >= timerBetweenAttack && fury >= 1f)
                    {
                        anim.SetTrigger("Special");
                        lastSpecial = Time.time;
                        canSpecial = false;
                        attackType = AttackType.SpecialHit;
                        timerBetweenAttack = 0.7f;
                        Instantiate(egg, gameObject.transform.position + transform.up, Quaternion.identity);
                        player_script.playerFuryBar.fillAmount = 0f;
						specialChicken.Play();
                    }
                    break;
                case CurrentCharacter.penguin:
                    // Light attack
                    if (Input.GetButtonDown("J" + player + "_Light_" + gPad) && Time.time > lightCool && canLight && 0 >= timerBetweenAttack)
                    {
                        lastLight = Time.time;
                        lightHit.enabled = true;
                        canLight = false;
                        attackType = AttackType.LightHit;
                        timerBetweenAttack = 0.7f;
						lightPenguin.Play();
                    }
                    // Heavy attack
                    if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy && 0 >= timerBetweenAttack)
                    {
                        lastHeavy = Time.time;
                        heavyHit.enabled = true;
                        canHeavy = false;
                        attackType = AttackType.HeavyHit;
                        timerBetweenAttack = 0.8f;
						heavyPenguin.Play();
                    }
                    // Utility attack
                    if (Input.GetAxisRaw("J" + player + "_Mobility_" + gPad) > 0.3 && Time.time > utilityCool && canUtility && 0 >= timerBetweenAttack)
                    {
                        currentDashTime = maxDashTime;
                        isDashing = true;
                        lastUtility = Time.time;
                        canUtility = false;
                        attackType = AttackType.SpecialHit;
                        timerBetweenAttack = 0.7f;
						utilityPenguin.Play();
                    }
                    // Special attack
                    if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial && 0 >= timerBetweenAttack && fury >= 1f)
                    {
                        lastSpecial = Time.time;
                        currentDashTime = maxDashTime;
                        isDashing = true;
                        canSpecial = false;
                        isUlting = true;
                        attackType = AttackType.SpecialHit;
                        timerBetweenAttack = 0.7f;
                        player_script.playerFuryBar.fillAmount = 0f;
						specialPenguin.Play();
                    }
                    break;
                case CurrentCharacter.lion:
                    // Light attack
                    if (Input.GetButtonDown("J" + player + "_Light_" + gPad) && Time.time > lightCool && canLight && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Light");
                        lastLight = Time.time;
                        lightHit.enabled = true;
                        canLight = false;
                        attackType = AttackType.LightHit;
                        timerBetweenAttack = 0.7f;
						lightLion.Play();
                    }
                    // Heavy attack
                    if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.time > heavyCool && canHeavy && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Heavy");
                        lastHeavy = Time.time;
                        heavyHit.enabled = true;
                        canHeavy = false;
                        attackType = AttackType.HeavyHit;
                        timerBetweenAttack = 0.8f;
						heavyLion.Play();
                    }
                    // Utility attack
                    if (Input.GetAxisRaw("J" + player + "_Mobility_" + gPad) > 0.3 && Time.time > utilityCool && canUtility && 0 >= timerBetweenAttack)
                    {
                        anim.SetTrigger("Utility");
                        player_script.lionDmgReduceActive = true;
                        lastUtility = Time.time;
                        canUtility = false;
                        Invoke("LionNotReduceDmg", 1.5f);
                        timerBetweenAttack = 0.7f;
						utilityLion.Play();
                    }
                    // Special attack
                    if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial && 0 >= timerBetweenAttack && fury >= 1f)
                    {
                        anim.SetTrigger("Ultimate");
                        lastSpecial = Time.time;
                        canSpecial = false;
                        attackType = AttackType.SpecialHit;
                        timerBetweenAttack = 0.7f;
                        player_script.enemyPlayer.controller.stunned = true;
                        player_script.playerFuryBar.fillAmount = 0f;
						specialLion.Play();
                    }
                    break;
                default:
                    break;
            }

            //FastFall();
            ResetAbilities();
        }
    }

    //private void FastFall()
    //{
    //    if (rb.velocity.y < -0.01) { rb.AddForce(Vector3.down * 30, ForceMode.Acceleration); }
    //}

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
        if (isDashing == true)
        {
            if (isUlting == true)
            {
                isDashing = false;
                currentDashSpeed = 0;
                PenguinUlt();
                ultTimer = 1.5f;
            }
            else
            {
                currentDashSpeed = 1;
                isDashing = false;
            }
        }
    }
}