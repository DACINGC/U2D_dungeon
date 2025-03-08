using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("技能冷却")]
    public float cooldown;
    [Header("耗蓝量")]
    public float expend;
    protected bool skillUsed;
    protected bool canUse;
    private Player player;
    private float cooldownTimer;

    
    public virtual void Start()
    {
        UnLockedSkill();
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    { 
        cooldownTimer -= Time.deltaTime;
    }
    //加载时，解锁技能
    protected virtual void UnLockedSkill()
    { 
        
    }
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            cooldownTimer = cooldown;
            return true;
        }

        player.GetComponent<EntityFX>().PopText("Skill is cooldown", player.transform);
        Debug.Log("Skill is cooldown");
        return false;
    }
    public virtual void UseSkill(PlayerStat playerStat)
    {
        if (playerStat.currentBlueBbar - expend < 0)
        { 
            Debug.Log("蓝量不足");
            player.GetComponent<EntityFX>().PopText("Blue bar is not enough!", player.transform);
            canUse = false;
            return;
        }
        canUse = true;

        playerStat.currentBlueBbar -= expend;

        if (playerStat.currentBlueBbar < 0)
            playerStat.currentBlueBbar = 0;
        
        SkillManager.instance.gameUI.UpdateBlueUI();
    }
}
