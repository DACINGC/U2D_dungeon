using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Effect/Buff")]
public class BuffEffect : ItemEffect
{
    private PlayerStat playerStat;
    public StatType buffType;
    public float duraion;
    public int modifer;

    public override void ApplyEffect(Transform _targetTransform)
    {
        playerStat = PlayerManager.instance.player.stat;
        Inventory.instance.UseBuff(playerStat.GetStat(buffType), duraion, modifer);
    }
}
