using System.Text;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemTypes
{ 
    Material,
    Equipment
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public ItemTypes itemType;
    public string itemName;
    public Sprite itemSprite;
    public int ItemNumber;
    public string itemId;


    [Range(0, 1f)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        //��һ��ֻ��Unity�༭�����ܹ�ʹ��
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif

    }
    public virtual string GetDescription()
    {
        return "";
    }
    
}
