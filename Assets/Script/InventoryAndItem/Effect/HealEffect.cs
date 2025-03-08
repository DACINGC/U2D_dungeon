using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Effect/Heal Flask")]
public class HealEffect : ItemEffect
{
    [Range(0, 1f)]
    public float healPercent;
    public override void ApplyEffect(Transform _targetTransform)
    {
        PlayerStat playerStat = PlayerManager.instance.player.stat;

        int healAount = Mathf.RoundToInt(playerStat.maxHp.GetValue() * healPercent);

        playerStat.IncreaseHealthBy(healAount);
    }
}
