using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("ÕÊº“µÙ¬‰ŒÔ∆∑")]
    [SerializeField] private float chanceToLoseEquipment;
    [SerializeField] private float changeToLoseMaterial;

    [Range(0, 1)]
    [SerializeField] private float cashDropPercent;

    public void PlayerCashDrop()
    {
        PlayerManager.instance.currentCash -= Mathf.RoundToInt(PlayerManager.instance.currentCash * cashDropPercent);
    }

    public override void GeneralDrop()
    {
        Inventory inventory = Inventory.instance;

        List<Item_Equipment> equipmentToDrop = new List<Item_Equipment>();

        foreach (var itemType in inventory.GetEquipment())
        {
            if (Random.Range(0, 100) < chanceToLoseEquipment)
            {
                DropItem(itemType.item);
                equipmentToDrop.Add(itemType.item as Item_Equipment);
            }
        }

        for (int i = 0; i < equipmentToDrop.Count; i++)
        {
            inventory.UnEquipItem(equipmentToDrop[i]);
            inventory.UpdateSlotUI();
        }

        List<ItemType> materialToDrop = new List<ItemType>();

        foreach (var itemType in inventory.GetInventory())
        {
            if (Random.Range(0, 100) < changeToLoseMaterial)
            {
                DropItem(itemType.item);
                materialToDrop.Add(itemType);
            }
        }

        for (int i = 0; i < materialToDrop.Count; i++)
        {
            inventory.RemoveItem(materialToDrop[i].item);
        }

    }
}
