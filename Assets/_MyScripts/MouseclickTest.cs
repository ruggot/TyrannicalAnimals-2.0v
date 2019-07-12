using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseclickTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            Debug.Log("Hello Whirld");
        }

    }
}
