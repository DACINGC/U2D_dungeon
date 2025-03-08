using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{
    private Player player;
    [Header("À¶Ìõ")]
    public Stat maxBlueBar;

    public float currentBlueBbar;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
        currentBlueBbar = GetValueOFBlueBar();
        OnHealthChanged?.Invoke();
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        if (_damage > (int)currentHp * 0.3)
            player.fX.ScreenShake(player.fX.heavyDamageShake);

        player.DamageEffect();

    }
    public override void DoDamage(CharacterStat _targetStat)
    {
        base.DoDamage(_targetStat);
        player.fX.ScreenShake(player.fX.damageShake);
        

    }
    protected override void Die()
    {
        base.Die();
        player.Die();
        GetComponent<PlayerItemDrop>().GeneralDrop();
        GetComponent<PlayerItemDrop>().PlayerCashDrop();
        AudioManager.instance.PlaySFX(60, player.transform, false);

    }

    public float GetValueOFBlueBar()
    {
        float maxblueBarValue = maxBlueBar.GetValue() + intelligence.GetValue() * 10;
        return maxblueBarValue;
    }
}
