using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggles : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    private void AnimationTriggle()
    {
        player.AnimationTriggle();
    }

    private void AttacTriggler()
    {
        AudioManager.instance.PlaySFX(2 , player.transform, true);
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().isDead)
                    return;

                EnemyStat target = hit.GetComponent<EnemyStat>();
                player.stat.DoDamage(target);

                AudioManager.instance.PlaySFX(1, player.transform, true);
                Item_Equipment sword = Inventory.instance.GetEquipmentType(EquipmentType.Sword);

                if (sword != null)
                {
                    sword.ApplyItemEffect(hit.transform);
                }

            }
        }

    }

    private void FireAttack()
    {
        if (SkillManager.instance.charge.fireAttackUnlocked)
        {
            AudioManager.instance.PlaySFX(87, player.transform, false);
            SkillManager.instance.fireAttack.UseSkill(player.stat);
        }
    }


}
