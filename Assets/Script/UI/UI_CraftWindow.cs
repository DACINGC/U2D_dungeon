using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private Image itemIcorn;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Image[] materialsImage;

    [SerializeField] protected Button craftButton;

    public void SetUpCraftWindow(Item_Equipment craftEquipment)
    {
        craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialsImage.Length; i++)
        {
            materialsImage[i].color = Color.clear;
            materialsImage[i].GetComponentInChildren<Text>().color = Color.clear;
        }

        for (int i = 0; i < craftEquipment.craftMaterials.Count; i++)
        {
            materialsImage[i].sprite = craftEquipment.craftMaterials[i].item.itemSprite;
            materialsImage[i].color = Color.white;

            Text amount = materialsImage[i].GetComponentInChildren<Text>();
            amount.color = Color.white;
            amount.text = craftEquipment.craftMaterials[i].stackSize.ToString();
        }

        itemIcorn.sprite = craftEquipment.itemSprite;
        itemName.text = craftEquipment.itemName;
        itemDescription.text = craftEquipment.GetDescription();

        craftButton.onClick.AddListener(() => Inventory.instance.CanCraft(craftEquipment,craftEquipment.craftMaterials));
    }
}
