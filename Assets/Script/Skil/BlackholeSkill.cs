using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackholeSkill : Skill
{
    [Header("ºÚ¶´²ÎÊý")]
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxSize;
    [SerializeField] private float damageTime;
    [SerializeField] private float duration;

    [Header("ºÚ¶´¹¥»÷")]
    public bool blackholeLocked;
    [SerializeField] private UI_SkillSlot blackholeLockedButton;

    private BlackholeSkillController currentBlackhole;
    public override void Start()
    {
        base.Start();
        blackholeLockedButton.GetComponent<Button>().onClick.AddListener(UnlockedBlackhole);
    }
    protected override void UnLockedSkill()
    {
        base.UnLockedSkill();
        UnlockedBlackhole();
    }
    private void UnlockedBlackhole()
    {
        if (blackholeLockedButton.unlocked)
            blackholeLocked = true;
    }

    public bool BlackHoleFinished()
    {
        if (!currentBlackhole)
            return true;

        return false;
    }

    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill( playerStat);
        if (!canUse)
            return;
        Player player = PlayerManager.instance.player;

        GameObject newBlachole = Instantiate(blackholePrefab, player.transform.position + new Vector3(2 * player.facingDir, 0, 0), Quaternion.identity);
        currentBlackhole = newBlachole.GetComponent<BlackholeSkillController>();
        currentBlackhole.SetUpBlackhole(growSpeed, maxSize, damageTime, duration);
    }
}

