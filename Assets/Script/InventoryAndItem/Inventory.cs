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
    [Header("��ʼ��Ʒ")]
    public List<ItemType> startItem;
    [Header("���Ͽ��")]
    public List<ItemType> itemType;
    public Dictionary<Item, ItemType> materialDictionary;

    [Header("�������")]
    public List<ItemType> stashType;
    public Dictionary<Item, ItemType> weaponDictionary;

    [Header("װ�����")]
    public List<ItemType> equipmentType;
    public Dictionary<Item_Equipment, ItemType> equipmentDictionary;

    [Header("���UI")]
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    [Header("UI��")]
    public UI_ItemSlot[] materialSlot;
    public UI_ItemSlot[] weaponSlot;
    public UI_EquipmentSlot[] equipmentSlot;
    public UI_StatSlot[] statSlot;

    [Header("���ݿ�")]
    public List<ItemType> loadedItem;
    public List<Item_Equipment> loadedEquipmet;

    [Header("����װ��")]
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
            //����Ѿ�װ���ϵ�װ�������Ƿ�ͽ�Ҫװ������������һ��
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }
        //��װ������װ��ж�£�����������
        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        //����װ����ӵ���������
        equipmentType.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        //�ӿ�����Ƴ���װ��
        RemoveItem(_item);

        newEquipment.AddModifers();
        UpdateSlotUI();
        PlayerManager.instance.player.stat.OnHealthChanged.Invoke();
    }
    public bool CanAddItem()
    {
        if (stashType.Count >= weaponSlot.Length)
        {
            Debug.Log("�������");
            return false;
        }
        return true;
    }
    public bool CanAddToMaterial()
    {
        if (itemType.Count > materialSlot.Length)
        {
            Debug.Log("��������");
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
        //��������װ����UI
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

        //����ǰ�������ڵ���Ʒ���

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
                    Debug.Log("���ϲ���");
                    return false;
                }
                else
                {
                    materialToRemove.Add(_requireMaterials[i]);
                }
            }
            else 
            {
                Debug.Log("û���ҵ�����");
                return false;
            }
        }
        for (int i = 0; i < materialToRemove.Count; i++)
        {
            RemoveItem(materialToRemove[i].item);
        }
        AddItem(_itemToCraft);
        Debug.Log(_itemToCraft.itemName + "�����ɹ�");
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

    //ʹ��ҩˮ
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
                //Ѱ����Դ��������ļ���Ҳ�ҳ���(�鿴Ҫ���ص���Ʒ�Ƿ������Ӧ��UID)
                if (item != null && item.itemId == pair.Key)
                {
                    ItemType itemToLoad = new ItemType(item);
                    itemToLoad.stackSize = pair.Value;
                    //��Ҫ������Ʒ����Ϣ�洢���б���
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
        //��������
        foreach (KeyValuePair<Item, ItemType> weapon in weaponDictionary)
        {
            _data.inventory.Add(weapon.Key.itemId, weapon.Value.stackSize);
        }
        //�������
        foreach (KeyValuePair<Item, ItemType> material in materialDictionary)
        {
            _data.inventory.Add(material.Key.itemId, material.Value.stackSize);
        }
        //����װ��
        foreach (KeyValuePair<Item_Equipment, ItemType> equipment in equipmentDictionary)
        {
            _data.equipmentId.Add(equipment.Key.itemId);
        }

    }

#if UNITY_EDITOR
    [ContextMenu("����Ҫ���ص�װ��")]
    private void FillUpItemDataBase() => itemDataBase = new List<Item>(GetItemDataBase());

    //AssetDatabase������Unity�ڹ�����Ϸʱ����
    //�ҵ���Դ�ļ����µ���Ʒ
    private List<Item> GetItemDataBase()
    { 
        List<Item> itemDataBase = new List<Item>();

        //�ҵ�ָ���ļ����µ�������Դ�� GUID
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
