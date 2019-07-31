using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected Transform mainCamera;

    public GameObject player1;
    public GameObject player2;

    private Vector3 offset = new Vector3(0, 20, -20);

    private Vector3 lastOffset;
    private Vector3 midpoint;
    private float pDist;
    private Vector3 newCamPos;

    public GameObject Player1 { get => player1; set => player1 = value; }
    public GameObject Player2 { get => player2; set => player2 = value; }

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
        pDist = Vector3.Distance(Player1.GetComponentsInChildren<Transform>(false)[0].position, Player2.GetComponentsInChildren<Transform>(false)[0].position);
        lastOffset = offset;
        offset.Set(offset.x, pDist / 3, -pDist);
        midpoint = (Player1.GetComponentsInChildren<Transform>(false)[0].position + Player2.GetComponentsInChildren<Transform>(false)[0].position) / 2;
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
