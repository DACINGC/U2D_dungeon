using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatToolTip :UI_ToolTip
{
    [SerializeField] private Text descriptionText;

    public void ShowToolTip(string _text)
    { 
        descriptionText.text = _text;
        gameObject.SetActive(true);
        AdjustPosition();
    }

    public void HideToolTip()
    {
        descriptionText.text = "";
        gameObject.SetActive(false);
    }
}
