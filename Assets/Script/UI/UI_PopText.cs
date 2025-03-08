using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopText : MonoBehaviour
{
    private TextMeshPro text;
    [SerializeField] private float speed;
    [SerializeField] private float desapearSpeed;
    [SerializeField] private float colorLoosingSpeed;
    [SerializeField] private float lifeTime;
    private float textTime;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        textTime = lifeTime;
    }
    private void Update()
    {
        textTime -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 0.5f), speed * Time.deltaTime);

        if (textTime < 0)
        { 
            float alpha = text.color.a - colorLoosingSpeed * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            if(text.color.a < 50)
                speed = desapearSpeed;

            if (text.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
