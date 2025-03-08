using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemTypeText;
    [SerializeField] private Text itemDescriptionText;

    //private int defaultSize = 40;

    public void ShowToolTip(Item_Equipment _item)
    {

        gameObject.SetActive(true);
        AdjustPosition();

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.itemType.ToString();
        itemDescriptionText.text = _item.GetDescription();

        //if (itemNameText.text.Length > 18)
        //    itemNameText.fontSize = Mathf.RoundToInt(itemNameText.fontSize * 0.7f);
        //else
        //    itemNameText.fontSize = defaultSize;

    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
