using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType
{ 
    Sword,
    Amor,
    Anulet,
    Flask,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New equipment")]
public class Item_Equipment : Item
{
    public EquipmentType equipmentType;
    public ItemEffect[] itemEffects;
    public float cooldown;
    [Header("主要属性")]
    public int strength;//增加伤害
    public int agility;//增加闪避率
    public int damage;//基础伤害值
    public int intelligence;//增加魔法 1点魔法攻击 和 2点魔法防御

    [Header("防御属性")]
    public int maxHp;
    public int armor;//护甲
    public int evation;//闪避率

    [Header("暴击")]
    public int critChance;//暴击率
    public int critPower;//暴击伤害

    [Header("魔法攻击")]
    public int magicPower;

    [Header("制作材料")]
    public List<ItemType> craftMaterials;

    private int descriptionLine;
    public void ApplyItemEffect(Transform _targetTransform)
    { 
        foreach (var effect in itemEffects)
        {
            effect.ApplyEffect(_targetTransform);
        }
    }
    public void AddModifers()
    {
        PlayerStat playerStat = PlayerManager.instance.player.stat;
        playerStat.strength.AddModifiers(strength);
        playerStat.agility.AddModifiers(agility);
        playerStat.damage.AddModifiers(damage);
        playerStat.intelligence.AddModifiers(intelligence);
        playerStat.maxHp.AddModifiers(maxHp);
        playerStat.armor.AddModifiers(armor);
        playerStat.evation.AddModifiers(evation);
        playerStat.critChance.AddModifiers(critChance);
        playerStat.critPower.AddModifiers(critPower);
        playerStat.magicPower.AddModifiers(magicPower);
    }

    public void RemoveModifers()
    {
        PlayerStat playerStat = PlayerManager.instance.player.stat;
        playerStat.strength.RemoveModifiers(strength);
        playerStat.agility.RemoveModifiers(agility);
        playerStat.damage.RemoveModifiers(damage);
        playerStat.intelligence.RemoveModifiers(intelligence);
        playerStat.maxHp.RemoveModifiers(maxHp);
        playerStat.armor.RemoveModifiers(armor);
        playerStat.evation.RemoveModifiers(evation);
        playerStat.critChance.RemoveModifiers(critChance);
        playerStat.critPower.RemoveModifiers(critPower);
        playerStat.magicPower.RemoveModifiers(magicPower);
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLine = 0;
        AddItemToDescription(strength, "力量");
        AddItemToDescription(agility, "敏捷");
        AddItemToDescription(damage, "伤害");
        AddItemToDescription(intelligence, "智力");
        AddItemToDescription(maxHp, "最大生命");
        AddItemToDescription(armor, "护甲");
        AddItemToDescription(evation, "闪避");
        AddItemToDescription(critChance, "暴击率");
        AddItemToDescription(critPower, "暴击伤害");
        AddItemToDescription(magicPower, "魔法伤害");

        if (descriptionLine < 5)
        {
            for (int i = 0; i < 5 - descriptionLine; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }

    public void AddItemToDescription(int _value, string _name)
    { 
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.Append("+" +_name + ": " + _value);

            descriptionLine++;
        }
    }
}
