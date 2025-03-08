using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockShrickController : MonoBehaviour
{
    private void ShockAnimationTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                PlayerStat playerStat = PlayerManager.instance.player.stat;

                playerStat.DoMagicDamage(hit.GetComponent<CharacterStat>(), 30);
            }
        }
    }
}
