using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    private Enemy enemy;
    public Stat cashAmount;
    public int leve = 1;
    [Range(0, 1f)]
    public float theModifer;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    protected override void Start()
    {
        cashAmount.SetDefaultValue(4);

        Modifer(cashAmount);
        Modifer(damage);
        Modifer(maxHp);
        base.Start();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        enemy.DamageEffect();
    }
    
    public void Modifer(Stat _stat)
    {
        for (int i = 0; i < leve; i++)
        { 
            float modifer = _stat.GetValue() * theModifer;
            _stat.AddModifiers(Mathf.RoundToInt(modifer));
        }

    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }

}
