using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    strength,
    agility,
    damage,
    intelligence,
    maxHp,
    armor,
    evation,
    critChance,
    critPower,
    magicPower,

}

public class CharacterStat : MonoBehaviour
{
    [Header("��Ҫ����")]
    public Stat strength;//�����˺�(��1���˺���1�㱩���ʣ�2�㱩���˺�)
    public Stat agility;//����������
    public Stat damage;//�����˺�ֵ
    public Stat intelligence;//����ħ�� 1��ħ������ �� 2��ħ������ ���ӽ�ɫ������

    [Header("��������")]
    public Stat maxHp;
    public Stat armor;//����
    public Stat evation;//������

    [Header("����")]
    public Stat critChance;//������
    public Stat critPower;//�����˺�

    [Header("ħ������")]
    public Stat magicPower;

    public int currentHp;
    public bool isDead { get; private set; }
    public System.Action OnHealthChanged;
    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();

        critPower.SetDefaultValue(150);
        magicPower.SetDefaultValue(5);

        //OnHealthChanged();
    }
    private bool isInvincible;
    public void MakeInvincible(bool _isInvincible)
    {
        isInvincible = _isInvincible;
    }
    private bool CanCrit()
    {
        if (Random.Range(0, 100) < critChance.GetValue() + strength.GetValue())
            return true;

        return false;
    }

    private int CalculateCritDamage(int _damamge)
    { 
        float critDamge = _damamge * (critPower.GetValue() + strength.GetValue() * 2)* 0.01f;
        return Mathf.RoundToInt(critDamge);
    }
    public virtual void DoDamage(CharacterStat _targetStat)
    {
        bool critical = false;
        //�����ܻ�����
        _targetStat.GetComponent<Entity>().SetUpRepelDir(transform);

        //�Ƿ�������ܹ���
        if (TargetCanAvoidAttack(_targetStat))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        //�Ƿ���Ա���
        if (CanCrit())
        {
            critical = true;
            totalDamage = CalculateCritDamage(totalDamage);
            Debug.Log("����"+totalDamage);
        }

        //ʵ����������Ч
        GetComponent<EntityFX>().CreateHitAnim(_targetStat.transform, critical);
        //���׵����˺�
        totalDamage = CheckTargetArmor(_targetStat, totalDamage);

        //�����ı�
        GetComponent<EntityFX>().PopText(totalDamage.ToString(), _targetStat.transform);

        _targetStat.TakeDamage(totalDamage);
    }
    public virtual void DoMagicDamage(CharacterStat _targetStat, int skillDamage)
    {
        //�Ƿ�������ܹ���
        if (TargetCanAvoidAttack(_targetStat))
            return;

        AudioManager.instance.PlaySFX(85, transform, false);

        int totalMagicDamage = magicPower.GetValue() + intelligence.GetValue() - _targetStat.intelligence.GetValue() * 2 + skillDamage;

        _targetStat.TakeDamage(totalMagicDamage);
        GetComponent<EntityFX>().PopText(totalMagicDamage.ToString(), _targetStat.transform);
        _targetStat.GetComponent<Entity>().DamageEffect();
    }
    public void IncreaseHealthBy(int _increaseHP)
    {
        currentHp += _increaseHP;

        if(currentHp > maxHp.GetValue())
            currentHp = maxHp.GetValue();

        if (OnHealthChanged != null)
            OnHealthChanged();
    }
    private static int CheckTargetArmor(CharacterStat _targetStat, int totalDamage)
    {
        totalDamage -= _targetStat.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private static bool TargetCanAvoidAttack(CharacterStat _targetStat)
    {
        int toltalEvation = _targetStat.evation.GetValue() + _targetStat.agility.GetValue();
        if (Random.Range(0, 100) < toltalEvation)
        { 
            Debug.Log("Avoid Attack");
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
        {
            Debug.Log("�����޵�״̬");
            return;
        }
        currentHp -= _damage;

        OnHealthChanged();
        if (currentHp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    public Stat GetStat(StatType buffType)
    {
        if (buffType == StatType.strength) return strength;
        else if (buffType == StatType.agility) return agility;
        else if (buffType == StatType.damage) return damage;
        else if (buffType == StatType.intelligence) return intelligence;
        else if (buffType == StatType.maxHp) return maxHp;
        else if (buffType == StatType.armor) return armor;
        else if (buffType == StatType.evation) return evation;
        else if (buffType == StatType.critChance) return critChance;
        else if (buffType == StatType.critPower) return critPower;
        else if (buffType == StatType.magicPower) return magicPower;

        return null;
    }
}
