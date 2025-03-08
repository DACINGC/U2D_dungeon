using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    public bool dashLocked;
    [SerializeField] private UI_SkillSlot dashLockedButton;

    public override void Start()
    {
        base.Start();
        dashLockedButton.GetComponent<Button>().onClick.AddListener(UnLockedDash);
    }

    protected override void UnLockedSkill()
    {
        base.UnLockedSkill();
        UnLockedDash();
    }
    private void UnLockedDash()
    {
        if (dashLockedButton.unlocked)
            dashLocked = true;
    }
    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);
        if (!canUse)
            return;
    }
}
