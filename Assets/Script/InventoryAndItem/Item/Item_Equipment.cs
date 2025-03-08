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
    [Header("��Ҫ����")]
    public int strength;//�����˺�
    public int agility;//����������
    public int damage;//�����˺�ֵ
    public int intelligence;//����ħ�� 1��ħ������ �� 2��ħ������

    [Header("��������")]
    public int maxHp;
    public int armor;//����
    public int evation;//������

    [Header("����")]
    public int critChance;//������
    public int critPower;//�����˺�

    [Header("ħ������")]
    public int magicPower;

    [Header("��������")]
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
        AddItemToDescription(strength, "����");
        AddItemToDescription(agility, "����");
        AddItemToDescription(damage, "�˺�");
        AddItemToDescription(intelligence, "����");
        AddItemToDescription(maxHp, "�������");
        AddItemToDescription(armor, "����");
        AddItemToDescription(evation, "����");
        AddItemToDescription(critChance, "������");
        AddItemToDescription(critPower, "�����˺�");
        AddItemToDescription(magicPower, "ħ���˺�");

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
