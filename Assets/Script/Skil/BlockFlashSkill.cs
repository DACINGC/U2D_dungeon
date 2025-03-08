using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlashSkill : Skill
{
    [SerializeField] private GameObject blackFlash;

    
    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);
        if (!canUse)
            return;
        Player player = PlayerManager.instance.player;
        Instantiate(blackFlash, new Vector2(player.transform.position.x + player.facingDir * 0.7f, player.transform.position.y + 0.4f), Quaternion.identity);
    }
}
