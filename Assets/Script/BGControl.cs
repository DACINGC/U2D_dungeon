using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGControl : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float xOffset;
    private float xPos;//不加上，会使得背景无限滚动
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - xOffset);
        float distanceTomove = cam.transform.position.x * xOffset;

        transform.position = new Vector2(xPos + distanceTomove, transform.position.y);

        if (distanceMoved > xPos + length)
            xPos += length;
        else if (distanceMoved < xPos - length)
            xPos -= length;
    }
}   
