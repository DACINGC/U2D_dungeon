using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    public void SetUpCraftSlot(Item_Equipment _item)
    { 
        itemType.item = _item;
        itemImage.sprite = _item.itemSprite;
        itemText.text = _item.itemName;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetUpCraftWindow(itemType.item as Item_Equipment);
    }
}
