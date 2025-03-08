using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDashClone : MonoBehaviour
{
    private SpriteRenderer sr;
    private float desapearRate;

    public void SetUpDashClone(float _losingRate, Sprite _sr)
    { 
        sr = GetComponent<SpriteRenderer>();
        desapearRate = _losingRate;
        sr.sprite = _sr;
    }
    private void Update()
    {
        float alpha = sr.color.a - Time.deltaTime * desapearRate;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (alpha <= 0)
            Destroy(gameObject);
    }
}
