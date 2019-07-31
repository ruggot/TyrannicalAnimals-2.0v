// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ChickenController : PlayerController
// {

//     protected Animator anim;
//     protected Rigidbody rb;

//     void Start()
//     {
//         anim = GetComponent<Animator>();
//         rb = GetComponent<Rigidbody>();
//     }

//     // Update is called once per frame
//     void Update()
//     {

//         //ControlPlayer();
//         //float h = CrossPlatformInputManager.GetAxis("J" + inputJoystick + "_Horizontal_" + gampadType);
//         //float v = CrossPlatformInputManager.GetAxis("J" + inputJoystick + "_Vertical_" + gampadType);
//         //jump = CrossPlatformInputManager.GetButton("J" + inputJoystick + "_Jump_" + gampadType);
//         //lightAttack = CrossPlatformInputManager.GetButton("J" + inputJoystick + "_Light_" + gampadType);
//         //heavyAttack = CrossPlatformInputManager.GetButton("J" + inputJoystick + "_Heavy_" + gampadType);
//         //specialAttack = CrossPlatformInputManager.GetButton("J" + inputJoystick + "_Special_" + gampadType);
//         //// linus added this
//         //// need some fixing
//         // if (Input.GetButtonDown("J" + player + "_Light_" + gPad)
//         {
//             anim.SetTrigger("Peck");

//         }
//         if (Input.GetButtonDown("J" + player + "_Special_" + gPad))
//         {
//             anim.SetTrigger("Special");
//         }
//         if (Input.GetButtonDown("J" + player + "_Heavy_" + gPad))
//         {
//             anim.SetTrigger("Heavy");
//         }
//     }
// }
