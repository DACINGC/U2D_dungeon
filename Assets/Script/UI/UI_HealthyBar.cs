using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthyBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private RectTransform rectTransform;
    private Slider slider;
    private CharacterStat stat =>GetComponentInParent<CharacterStat>();
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        entity.OnFlipped += FlipHealthyBar;
        stat.OnHealthChanged += UpdateHealthBar;
    }
    void Start()
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        slider.maxValue = stat.maxHp.GetValue();
        slider.value = stat.currentHp;
    }

    private void FlipHealthyBar()
    {
        rectTransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        if(entity != null)
            entity.OnFlipped -= FlipHealthyBar;

        if(stat != null)
            stat.OnHealthChanged -= UpdateHealthBar;
    }
}
