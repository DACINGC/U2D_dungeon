using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSkill : Skill
{
    [Header("ÐîÁ¦¹¥»÷")]
    public bool chargeUnlocked;
    [SerializeField] private UI_SkillSlot chargeUnlockedButton;

    [Header("³å»÷²¨")]
    public bool blockFlashUnlocked;
    [SerializeField] private UI_SkillSlot blockUnlockedButton;

    [Header("ÐîÁ¦½£Æø")]
    public bool swordLigtUnlocked;
    [SerializeField] private UI_SkillSlot swordUnlockedButton;

    [Header("±¬Õ¨½£Æø")]
    public bool burstLigtUnloced;
    [SerializeField] private UI_SkillSlot burstLightUnlockedButton;

    [Header("´©Í¸½£Æø")]
    public bool pierceSowrdUnlocked;
    [SerializeField] private UI_SkillSlot pierceSowrdUnlockedButton;

    [Header("»ðÑæ¹¥»÷")]
    public bool fireAttackUnlocked;
    [SerializeField] private UI_SkillSlot fireAttackUnlockedButton;

    public override void Start()
    {
        base.Start();

        chargeUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedChargeAttack);
        blockUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedBlockFlash);
        swordUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedSwordLight);
        fireAttackUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedFireAttack);
        burstLightUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlokedBurstLight);
        pierceSowrdUnlockedButton.GetComponent<Button>().onClick.AddListener(UnlockedPierceSword);

    }

    protected override void UnLockedSkill()
    {
        base.UnLockedSkill();
        UnlockedChargeAttack();
        UnlockedBlockFlash();
        UnlockedFireAttack();
        UnlockedPierceSword();
        UnlockedSwordLight();
        UnlokedBurstLight();
    }
    private void UnlockedChargeAttack()
    {

        if (chargeUnlockedButton.unlocked)
        {
            chargeUnlocked = true;
        }
    }

    private void UnlockedBlockFlash() 
    {
        if(blockUnlockedButton.unlocked)
            blockFlashUnlocked = true;
    }

    private void UnlockedSwordLight()
    { 
        if(swordUnlockedButton.unlocked)
            swordLigtUnlocked = true;
    }

    private void UnlockedFireAttack()
    {
        if(fireAttackUnlockedButton.unlocked)
            fireAttackUnlocked = true;
    }

    private void UnlokedBurstLight()
    {
        if (burstLightUnlockedButton.unlocked)
        { 
            burstLigtUnloced = true;
            SkillManager.instance.swordLight.type = SwordLigtType.burst;
        }
    }

    private void UnlockedPierceSword()
    {
        if (pierceSowrdUnlockedButton.unlocked)
        { 
            pierceSowrdUnlocked = true;
            SkillManager.instance.swordLight.type = SwordLigtType.pierce;
        }
    }

    public override void UseSkill(PlayerStat playerStat)
    {
        base.UseSkill(playerStat);
        if (!canUse)
            return;
    }
}
