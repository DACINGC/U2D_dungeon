using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equpmentType;

    private void OnValidate()
    {
        transform.name = "Equipment: "+ equpmentType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemType.item == null || itemType == null)
            return;

        Inventory.instance.UnEquipItem(itemType.item as Item_Equipment);
        Inventory.instance.AddItem(itemType.item as Item_Equipment);
        CleanUpSlot();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base .OnPointerExit(eventData);
    }
}
