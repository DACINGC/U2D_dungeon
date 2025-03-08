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
    [Header("主要属性")]
    public Stat strength;//增加伤害(加1点伤害，1点暴击率，2点暴击伤害)
    public Stat agility;//增加闪避率
    public Stat damage;//基础伤害值
    public Stat intelligence;//增加魔法 1点魔法攻击 和 2点魔法防御 增加角色的蓝条

    [Header("防御属性")]
    public Stat maxHp;
    public Stat armor;//护甲
    public Stat evation;//闪避率

    [Header("暴击")]
    public Stat critChance;//暴击率
    public Stat critPower;//暴击伤害

    [Header("魔法攻击")]
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
        //设置受击方向
        _targetStat.GetComponent<Entity>().SetUpRepelDir(transform);

        //是否可以闪避攻击
        if (TargetCanAvoidAttack(_targetStat))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        //是否可以暴击
        if (CanCrit())
        {
            critical = true;
            totalDamage = CalculateCritDamage(totalDamage);
            Debug.Log("暴击"+totalDamage);
        }

        //实例化攻击特效
        GetComponent<EntityFX>().CreateHitAnim(_targetStat.transform, critical);
        //护甲抵御伤害
        totalDamage = CheckTargetArmor(_targetStat, totalDamage);

        //弹出文本
        GetComponent<EntityFX>().PopText(totalDamage.ToString(), _targetStat.transform);

        _targetStat.TakeDamage(totalDamage);
    }
    public virtual void DoMagicDamage(CharacterStat _targetStat, int skillDamage)
    {
        //是否可以闪避攻击
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
            Debug.Log("处于无敌状态");
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
