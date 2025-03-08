using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationController : MonoBehaviour
{
    private void BurstDamageTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Entity>().SetUpRepelDir(transform);
                PlayerManager.instance.player.stat.DoMagicDamage(hit.GetComponent<CharacterStat>(), 10);
                
            }
        }

    }

    private void FireAttackAnimation()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(12, 3.5f), 0);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().isDead)
                    return;

                EnemyStat target = hit.GetComponent<EnemyStat>();
                hit.GetComponent<Entity>().SetUpRepelDir(transform);
                PlayerManager.instance.player.stat.DoMagicDamage(target, 30);

            }
        }
    }

    private void BlockFlashAnimation()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.2f);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            { 
                hit.GetComponent<Entity>().SetUpRepelDir(transform);
                PlayerManager.instance.player.stat.DoMagicDamage(hit.GetComponent<CharacterStat>(), 5);
            }
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}


