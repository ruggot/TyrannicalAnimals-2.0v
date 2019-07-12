using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    private Vector3 offset = new Vector3(0, 20, -20);

    private Vector3 midpoint;
    private float pDist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pDist = Vector3.Distance(player1.position, player2.position);
        offset.Set(offset.x, pDist/3, -pDist);
        midpoint = (player1.position + player2.position) / 2;
        mainCamera.SetPositionAndRotation(midpoint + offset, mainCamera.rotation);
        mainCamera.LookAt(midpoint);

    }
}