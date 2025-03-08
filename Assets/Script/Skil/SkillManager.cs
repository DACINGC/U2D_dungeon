using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public BlackholeSkill blackhole { get; private set; }
    public FireSkill fire { get; private set; }
    public SwordLightSkill swordLight { get; private set; }
    public FireAttackSkill fireAttack { get; private set; }
    public ChargeSkill charge { get; private set; }
    public BlockFlashSkill blockFlash { get; private set; }
    public DashSkill dash { get; private set; }

    public UI_InGame gameUI;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;

        blackhole = GetComponent<BlackholeSkill>();
        fire = GetComponent<FireSkill>();
        swordLight = GetComponent<SwordLightSkill>();
        fireAttack = GetComponent<FireAttackSkill>();
        charge = GetComponent<ChargeSkill>();
        blockFlash = GetComponent<BlockFlashSkill>();
        dash = GetComponent<DashSkill>();
    }

}
