using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player 1") || other.transform.CompareTag("Player 2"))
        {
            player = other.GetComponentInParent<Player>();
            Debug.Log("This works");
            player.TakeDamage("special", player.specialDmg);
            Destroy(gameObject);
        }
    }
}
