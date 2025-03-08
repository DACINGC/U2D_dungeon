using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ButtonImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        if (image != null)
        {
            image.color = Color.clear;
        }

        GetComponent<Button>().onClick.AddListener(() => AudioManager.instance.PlaySFX(83, null, false));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.white;
        AudioManager.instance.PlaySFX(82, null, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.clear;
    }
}
