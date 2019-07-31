﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected Transform mainCamera;
    [SerializeField] protected Transform player1;
    [SerializeField] protected Transform player2;

    private Vector3 offset = new Vector3(0, 20, -20);

    private Vector3 lastOffset;
    private Vector3 midpoint;
    private float pDist;
    private Vector3 newCamPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateVars();
        {
            UpdateCameraPos();
        }
        mainCamera.LookAt(midpoint);
    }

    void UpdateVars()
    {
        pDist = Vector3.Distance(player1.position, player2.position);
        lastOffset = offset;
        offset.Set(offset.x, pDist / 3, -pDist);
        midpoint = (player1.position + player2.position) / 2;
    }

    void UpdateCameraPos()
    {
        // newCamPos = midpoint + offset;
        if (Vector3.Distance(midpoint + offset, midpoint) < 10)
        {
            offset = lastOffset;
        }
        newCamPos = midpoint + offset;
        mainCamera.SetPositionAndRotation(newCamPos, mainCamera.rotation);
    }
}