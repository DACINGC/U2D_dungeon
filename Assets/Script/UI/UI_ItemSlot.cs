using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected Text itemText;
    public ItemType itemType;
    public UI ui { get; private set; }

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }
    public void UpdateItemSlot(ItemType _itemType)
    { 
        itemType = _itemType;
        itemImage.color = Color.white;

        if (itemType != null)
        {
            itemImage.sprite = itemType.item.itemSprite;

            if (itemType.stackSize > 0)
                itemText.text = itemType.stackSize.ToString();
            else
                itemText.text = "";
        }
    }

    public void CleanUpSlot()
    {
        itemType = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
        
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(itemType == null)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        { 
            Inventory.instance.RemoveItem(itemType.item);
            return;
        }
                
        if (itemType.item.itemType == ItemTypes.Equipment)
            Inventory.instance.EquipItem(itemType.item);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (itemType == null || itemType.item == null || itemType.item.itemType == ItemTypes.Material)
            return;

        ui.itemToolTip.ShowToolTip(itemType.item as Item_Equipment);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (itemType == null)
            return;

        ui.itemToolTip.HideToolTip();
    }
}
