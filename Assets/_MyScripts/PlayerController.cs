using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 3;
    // collider for p1
    [SerializeField] protected BoxCollider lightHit;
    [SerializeField] protected BoxCollider heavyHit;
    [SerializeField] protected BoxCollider specialHit;
    [SerializeField] protected BoxCollider utilityHit;
    [SerializeField] protected BoxCollider bodyCollider;

    [SerializeField] protected float jumpForce;

    private float jumpCool = 1f;
    private float lightCool = 0.3f;
    private float heavyCool = 1.2f;
    private float utilityCool = 1.2f;
    private float specialCool = 1.2f;

    public float lastJump = 0f;
    public float lastLight = 0f;
    public float lastHeavy = 0f;
    public float lastUtility = 0f;
    public float lastSpecial = 0f;

    private bool canJump = true;
    private bool canLight = true;
    private bool canHeavy = true;
    private bool canUtility = true;
    private bool canSpecial = true;

    [SerializeField] protected string player;
    [SerializeField] protected string gPad;

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
        if (!SceneScript.paused)
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

            if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad) && Time.deltaTime > heavyCool && canHeavy)
            {
                anim.SetTrigger("Heavy");
                lastHeavy = Time.time + heavyCool;
                heavyHit.enabled = true;
                canHeavy = false;
            }

            /// Utility attack
            // Uncomment when utility added
            //            if (Input.GetButtonDown("J" + player + "_Utility_" + gPad) && Time.time > utilityDelay && canUtility)
            //            {
            //                anim.SetTrigger("Utility");
            //                sinceUtility = Time.time + utilityDelay;
            //            }

            if (Input.GetButtonDown("J" + player + "_Special_" + gPad) && Time.time > specialCool && canSpecial)
            {
                anim.SetTrigger("Special");
                lastSpecial = Time.time + specialCool;
                specialHit.enabled = true;
            }

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
        if (Time.time  >=  lastLight + lightCool) { lightHit.enabled = false; canLight = true; }
        if (Time.time  >=  lastHeavy + heavyCool) { heavyHit.enabled = false; canHeavy = true; }
        if (Time.time  >=  lastUtility + utilityCool) { utilityHit.enabled = false; canUtility = true; }
        if (Time.time  >=  lastSpecial + specialCool) { specialHit.enabled = false; canSpecial = true; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
        }
    }
}