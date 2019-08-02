using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrolling : MonoBehaviour
{
    public float xSpeed = 1f;
    public float ySpeed = 1f;
    public Renderer rend;
    private float curX;
    private float curY;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        curX = rend.material.mainTextureOffset.x;
        curY = rend.material.mainTextureOffset.y;
    }


    void FixedUpdate()
    {
        curX += Time.deltaTime * (xSpeed / 10);
        curY += Time.deltaTime * (ySpeed / 10);

        rend.material.SetTextureOffset("_MainTex", new Vector2(curX, curY));
    }
}
