using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;
    [Header("初始物品")]
    public List<ItemType> startItem;
    [Header("材料库存")]
    public List<ItemType> itemType;
    public Dictionary<Item, ItemType> materialDictionary;

    [Header("武器库存")]
    public List<ItemType> stashType;
    public Dictionary<Item, ItemType> weaponDictionary;

    [Header("装备库存")]
    public List<ItemType> equipmentType;
    public Dictionary<Item_Equipment, ItemType> equipmentDictionary;

    [Header("库存UI")]
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    [Header("UI槽")]
    public UI_ItemSlot[] materialSlot;
    public UI_ItemSlot[] weaponSlot;
    public UI_EquipmentSlot[] equipmentSlot;
    public UI_StatSlot[] statSlot;

    [Header("数据库")]
    public List<ItemType> loadedItem;
    public List<Item_Equipment> loadedEquipmet;

    [Header("加载装备")]
    public List<Item> itemDataBase;
    private float lastUesTimer;
    public float flaskCooldown {  get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    private void Start()
    {
        itemType = new List<ItemType>();
        materialDictionary = new Dictionary<Item, ItemType>();
        stashType = new List<ItemType>();
        weaponDictionary = new Dictionary<Item, ItemType>();
        equipmentType = new List<ItemType>();
        equipmentDictionary = new Dictionary<Item_Equipment, ItemType>();

        materialSlot = itemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        weaponSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        AddSartItem();

    }

    private void AddSartItem()
    {
        if (loadedEquipmet.Count > 0)
        { 
            foreach(var equipmrnt in loadedEquipmet)
                EquipItem(equipmrnt);
        }    


        if (loadedItem.Count > 0)
        {
            foreach (var itemType in loadedItem)
            {
                for (int i = 0; i < itemType.stackSize; i++)
                {
                    AddItem(itemType.item);
                }
            }

            return;
        }

        for (int i = 0; i < startItem.Count; i++)
        {
            AddItem(startItem[i].item);
        }

    }

    public void EquipItem(Item _item)
    { 
        Item_Equipment newEquipment = _item as Item_Equipment;
        ItemType newItem = new ItemType(newEquipment);

        Item_Equipment oldEquipment = null;

        foreach (KeyValuePair<Item_Equipment, ItemType> item in equipmentDictionary)
        {
            //检查已经装备上的装备类型是否和将要装备的武器类型一致
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }
        //将装备过的装备卸下，并放入库存中
        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        //将新装备添加到武器栏中
        equipmentType.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        //从库存中移除该装备
        RemoveItem(_item);

        newEquipment.AddModifers();
        UpdateSlotUI();
        PlayerManager.instance.player.stat.OnHealthChanged.Invoke();
    }
    public bool CanAddItem()
    {
        if (stashType.Count >= weaponSlot.Length)
        {
            Debug.Log("库存已满");
            return false;
        }
        return true;
    }
    public bool CanAddToMaterial()
    {
        if (itemType.Count > materialSlot.Length)
        {
            Debug.Log("材料已满");
            return false;
        }
        return true;
    }
    public void UnEquipItem(Item_Equipment itemToRemove)
    {
        if (itemToRemove == null)
            return;

        if (equipmentDictionary.TryGetValue(itemToRemove, out ItemType value))
        {
            equipmentType.Remove(value);
            itemToRemove.RemoveModifers();
            equipmentDictionary.Remove(itemToRemove);
        }
        PlayerManager.instance.player.stat.OnHealthChanged.Invoke();
    }
    public void UpdateSlotUI()
    {
        //更新武器装备的UI
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<Item_Equipment, ItemType> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].equpmentType)
                {
                    equipmentSlot[i].CleanUpSlot();
                    equipmentSlot[i].UpdateItemSlot(item.Value);
                }
            }
        }

        //更新前，将槽内的物品清空

        for (int i = 0; i < materialSlot.Length; i++)
        {
            materialSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < weaponSlot.Length; i++)
        {
            weaponSlot[i].CleanUpSlot();
        }


        for (int i = 0; i < itemType.Count; i++)
        {
            materialSlot[i].UpdateItemSlot(itemType[i]);
        }

        for (int i = 0; i < stashType.Count; i++)
        {
            weaponSlot[i].UpdateItemSlot(stashType[i]);
        }

        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public void UseBuff(Stat _stat, float _duration, int _modifer)
    {
        StartCoroutine(BuffDuration(_stat, _duration, _modifer));
    }
    private IEnumerator BuffDuration(Stat _stat, float _duration, int _modifer)
    {
        _stat.AddModifiers(_modifer);
        yield return new WaitForSeconds(_duration);
        _stat.RemoveModifiers(_modifer);
    }
    
    public void AddItem(Item _item)
    {
        if (_item == null)
            return;

        if(_item.itemType == ItemTypes.Material)
            AddToMaterial(_item);
        else if(_item.itemType == ItemTypes.Equipment && CanAddItem())
            AddToInventory(_item);

        UpdateSlotUI();
    }
    public bool CanCraft(Item_Equipment _itemToCraft, List<ItemType> _requireMaterials)
    { 
        List<ItemType> materialToRemove = new List<ItemType>();

        for (int i = 0; i < _requireMaterials.Count; i++)
        {
            if (materialDictionary.TryGetValue(_requireMaterials[i].item, out ItemType valule))
            {
                if (valule.stackSize < _requireMaterials[i].stackSize)
                {
                    Debug.Log("材料不足");
                    return false;
                }
                else
                {
                    materialToRemove.Add(_requireMaterials[i]);
                }
            }
            else 
            {
                Debug.Log("没有找到材料");
                return false;
            }
        }
        for (int i = 0; i < materialToRemove.Count; i++)
        {
            RemoveItem(materialToRemove[i].item);
        }
        AddItem(_itemToCraft);
        Debug.Log(_itemToCraft.itemName + "制作成功");
        return true;

    }
    private void AddToMaterial(Item _item)
    {
        if (materialDictionary.TryGetValue(_item, out ItemType value))
        {
            value.AddStack();
        }
        else
        {
            ItemType newItemType = new ItemType(_item);
            itemType.Add(newItemType);
            materialDictionary.Add(_item, newItemType);
        }
    }
    private void AddToInventory(Item _item)
    {
        if (weaponDictionary.TryGetValue(_item, out ItemType value))
        {
            value.AddStack();
        }
        else
        {
            ItemType newEquipment = new ItemType(_item);
            stashType.Add(newEquipment);
            weaponDictionary.Add(_item, newEquipment);
        }
    }

    public void RemoveItem(Item _item)
    {
        if (materialDictionary.TryGetValue(_item, out ItemType value))
        {
            if (value.stackSize <= 1)
            {
                itemType.Remove(value);
                materialDictionary.Remove(_item);
            }
            else
            { 
                value.RemoveStack();
            }
        }

        if (weaponDictionary.TryGetValue(_item,out ItemType equipment))
        {
            if (equipment.stackSize <= 1)
            {
                stashType.Remove(equipment);
                weaponDictionary.Remove(_item);
            }
            else
            { 
                equipment.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    public List<ItemType> GetEquipment() => equipmentType;
    public List<ItemType> GetInventory() => itemType;

    public Item_Equipment GetEquipmentType(EquipmentType _type)
    { 
        Item_Equipment equipment = null;
        foreach (KeyValuePair<Item_Equipment, ItemType> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipment = item.Key;
        }
        return equipment;
    }

    //使用药水
    public void UseFlask()
    { 
        Item_Equipment Flask = GetEquipmentType(EquipmentType.Flask);
        if (Flask == null)
            return;

        bool canUseFlask = Time.time > lastUesTimer + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = Flask.cooldown;
            Flask.ApplyItemEffect(null);
            lastUesTimer = Time.time;
        }
        else
        { 
            Debug.Log("Flask is cooldown");
            Player player = PlayerManager.instance.player;
            player.GetComponent<EntityFX>().PopText("Flask is cool down", player.transform);
        }
            

    }

    public void LoadData(GameData _data)
    {

        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                //寻找资源函数会把文件夹也找出来(查看要加载的物品是否存在相应的UID)
                if (item != null && item.itemId == pair.Key)
                {
                    ItemType itemToLoad = new ItemType(item);
                    itemToLoad.stackSize = pair.Value;
                    //把要加载物品的信息存储到列表中
                    loadedItem.Add(itemToLoad);
                }
            }
        }

        foreach (string equipment in _data.equipmentId)
        {
            foreach (var loadedItem in itemDataBase)
            {
                if (loadedItem != null && loadedItem.itemId == equipment)
                {
                    loadedEquipmet.Add(loadedItem as Item_Equipment);
                }
            }
        }


    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentId.Clear();
        //保存武器
        foreach (KeyValuePair<Item, ItemType> weapon in weaponDictionary)
        {
            _data.inventory.Add(weapon.Key.itemId, weapon.Value.stackSize);
        }
        //保存材料
        foreach (KeyValuePair<Item, ItemType> material in materialDictionary)
        {
            _data.inventory.Add(material.Key.itemId, material.Value.stackSize);
        }
        //保存装备
        foreach (KeyValuePair<Item_Equipment, ItemType> equipment in equipmentDictionary)
        {
            _data.equipmentId.Add(equipment.Key.itemId);
        }

    }

#if UNITY_EDITOR
    [ContextMenu("建立要加载的装备")]
    private void FillUpItemDataBase() => itemDataBase = new List<Item>(GetItemDataBase());

    //AssetDatabase会引起Unity在构建游戏时报错
    //找到资源文件夹下的物品
    private List<Item> GetItemDataBase()
    { 
        List<Item> itemDataBase = new List<Item>();

        //找到指定文件夹下的所有资源的 GUID
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Item/InventoryItem" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<Item>(SOpath);
            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    }
#endif
}
