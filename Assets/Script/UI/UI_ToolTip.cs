using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField]private float xLimit = 960;
    [SerializeField]private float ylimit = 540;

    [SerializeField] private float xOffset = 150;
    [SerializeField] private float yOffset = 150;

    public void AdjustPosition()
    {
        Vector2 mousePos = Input.mousePosition;
        float _xOffset;
        float _yOffset;

        if (mousePos.x > xLimit)
            _xOffset = -xOffset;
        else
            _xOffset = xOffset;

        if (mousePos.y > ylimit)
            _yOffset = -yOffset;
        else
            _yOffset = yOffset;

        transform.position = new Vector2(mousePos.x + _xOffset, mousePos.y + _yOffset);
    }
}
