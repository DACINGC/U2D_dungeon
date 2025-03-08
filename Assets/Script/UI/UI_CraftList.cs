using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Transform craftParent;
    [SerializeField] private GameObject craftPrefab;

    [SerializeField] private List<Item_Equipment> craftEquipmentList = new List<Item_Equipment>();
    [SerializeField] private List<UI_CraftSlot> craftSlotList = new List<UI_CraftSlot>();

    private void Start()
    {
        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetUpCraftList();
        SetupDefaultCraftWindow();
    }

    //建立工艺制作的对象
    public void SetUpCraftList()
    {
        for (int i = 0; i < craftParent.childCount; i++)
        {
            Destroy(craftParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < craftEquipmentList.Count; i++)
        {
            GameObject newSlot = Instantiate(craftPrefab, craftParent);
            newSlot.GetComponent<UI_CraftSlot>().SetUpCraftSlot(craftEquipmentList[i]);
            craftSlotList.Add(newSlot.GetComponent<UI_CraftSlot>());
        }

        SetupDefaultCraftWindow();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }

    public void SetupDefaultCraftWindow()
    {
        if (craftEquipmentList[0] != null)
            GetComponentInParent<UI>().craftWindow.SetUpCraftWindow(craftEquipmentList[0]);
    }
}
