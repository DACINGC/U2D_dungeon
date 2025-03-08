using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    [SerializeField]private Item item;
    [SerializeField]private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnValidate()
    {
        SetVailate();
    }

    private void SetVailate()
    {
        if (item == null)
            return;

        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        gameObject.name = "ItemObj: " + item.itemName;
    }

    public void SetUpItem(Item _item, Vector2 _velocity)
    { 
        item = _item;
        rb.velocity = _velocity;
        SetVailate();
    }
    public void PickUpItem()
    {
        if (!Inventory.instance.CanAddItem() && !Inventory.instance.CanAddToMaterial())
        {
            rb.velocity = new Vector2(0, 4);
            return;
        }

        AudioManager.instance.PlaySFX(18, transform, false);
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }

}
