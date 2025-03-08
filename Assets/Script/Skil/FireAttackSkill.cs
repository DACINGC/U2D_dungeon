using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireAttackSkill : Skill
{
    [SerializeField] private GameObject fireAttackPrefab;

    [Header("»ðÑæ¹¥»÷")]
    public bool fireAttackLocked;
    [SerializeField] protected UI_SkillSlot fireAttackLockedButton;

    public override void Start()
    {
        base.Start();
        fireAttackLockedButton.GetComponent<Button>().onClick.AddListener(UnlockedFireAttack);
    }
    protected override void UnLockedSkill()
    {
        base.UnLockedSkill();
        UnlockedFireAttack();
    }
    private void UnlockedFireAttack()
    {
        if (fireAttackLockedButton.unlocked)
            fireAttackLocked = true;
    }

    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);
        if (!canUse)
            return;
        Player player = PlayerManager.instance.player;

        GameObject fireAttack = Instantiate(fireAttackPrefab, new Vector2(player.transform.position.x + player.facingDir * 6f, player.transform.position.y), Quaternion.identity);
        fireAttack.GetComponent<FireAttackController>().SetUpFireAttack(player.facingDir);
    }
}
