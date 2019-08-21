using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] protected Transform mainCamera;

    private Vector3 offset = new Vector3(0, 20, -20);
    public float tweenSpeed = 3;
    private Vector3 lastOffset;
    private Vector3 midpoint;
    private Vector3 newMidpoint;
    private float pDist;
    private Vector3 newCamPos;
    private Vector3 n;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateVars();
        UpdateCameraPos();
        mainCamera.LookAt(midpoint);
    }

    void UpdateVars() {
        pDist = Vector3.Distance(DataManager.Player(0).transform.position, DataManager.Player(1).transform.position);
        lastOffset = offset;
        offset.Set(offset.x, pDist / 3, -pDist);
        newMidpoint = (DataManager.Player(0).transform.position + DataManager.Player(1).transform.position) / 2;
        midpoint = Tween(midpoint, newMidpoint);
    }

    void UpdateCameraPos() {
        // newCamPos = midpoint + offset;
        if (Vector3.Distance(midpoint + offset, midpoint) < 10) {
            offset = lastOffset;
        }
        newCamPos = midpoint + offset;
        // "Tweening" smooth movment effect
        //newCamPos = Tween(mainCamera.position, newCamPos);
        mainCamera.SetPositionAndRotation(newCamPos, mainCamera.rotation);
    }

    // "Tweening" smooth movment effect
    Vector3 Tween(Vector3 current, Vector3 next) {
        n = current + (next - current) * (tweenSpeed * Time.fixedDeltaTime); //, current.y + (next.y - current.y) * (tweener * Time.fixedDeltaTime), current.z + (next.z - current.z) * (tweener * Time.fixedDeltaTime));
        return n;
    }
}
