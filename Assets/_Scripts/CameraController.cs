using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected Transform mainCamera;

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
        pDist = Vector3.Distance(DataManager.Player(0).transform.position, DataManager.Player(1).transform.position);
        lastOffset = offset;
        offset.Set(offset.x, pDist / 3, -pDist);
        midpoint = (DataManager.Player(0).transform.position + DataManager.Player(1).transform.position) / 2;
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