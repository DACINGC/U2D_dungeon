using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    Enemy enemy => GetComponentInParent<Enemy>();


    protected virtual void AnimationTrigger()
    {
        enemy.stateMachine.currentState.AnimationFinishTriggle();
    }

    protected virtual void AttakTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStat target = hit.GetComponent<PlayerStat>();
                enemy.stat.DoDamage(target);
                AudioManager.instance.PlaySFX(Random.Range(47, 50), enemy.transform, false);

                if (hit.GetComponent<Player>().isCharging && SkillManager.instance.charge.blockFlashUnlocked)
                    SkillManager.instance.blockFlash.UseSkill(target);
            }
        }
    }
}
