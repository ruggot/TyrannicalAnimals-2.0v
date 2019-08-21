using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    public GameObject healthPack;
    public float heathTimer = 5;
    public float healthAdded = 0.2f;
    private float currentTimer;
    public bool healthSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!healthSpawned)
        {
            currentTimer = heathTimer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Activated healthpack
        healthPack.SetActive(healthSpawned);

        if (!healthSpawned)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer < 0)
            {
                healthSpawned = true;
                currentTimer = heathTimer;
            }
        }
        else
        {
            healthPack.transform.Rotate(0, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (healthSpawned)
        {
            // Add health to the player ("other")
            //other.GetComponent<Player>()."Health" += healthAdded;
            healthSpawned = false;
        }
    }
}
