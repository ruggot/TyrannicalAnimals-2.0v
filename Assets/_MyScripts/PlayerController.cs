using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 3;
    // collider for p1
    public GameObject p1LightHit;
    public GameObject p1HeavyHit;
    public GameObject p1Specialhit;

    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
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
            else
            {
                anim.SetInteger("Walk", 0);
            }

            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

            if (Input.GetButtonDown("J" + player + "_Jump_" + gPad) && Time.time > canJump)
            {
                rb.AddForce(0, jumpForce, 0);
                canJump = Time.time + timeBeforeNextJump;
                anim.SetTrigger("Jump");
            }

            if (Input.GetButtonDown("J" + player + "_Light_" + gPad))
            {
                anim.SetTrigger("Peck");

            }
            if (Input.GetButtonDown("J" + player + "_Special_" + gPad))
            {
                anim.SetTrigger("Special");
            }
            if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad))
            {
                anim.SetTrigger("Heavy");
            }
        }
    }
}