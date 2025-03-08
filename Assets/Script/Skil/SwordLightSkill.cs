using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLightSkill : Skill
{
    public SwordLigtType type;
    [SerializeField] GameObject swordLigtPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float desapearTime;

    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);

        if (!canUse)
            return;
        Player player = PlayerManager.instance.player;
        GameObject swordLight = Instantiate(swordLigtPrefab, new Vector2(player.transform.position.x + player.facingDir * 1.5f, player.transform.position.y), Quaternion.identity);
        swordLight.GetComponent<SwordLightController>().SetupSwordLigt(speed, desapearTime, player.facingDir, type);
    }
}
