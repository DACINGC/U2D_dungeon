using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeAttackState : PlayerGroundState
{
    public PlayerChargeAttackState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (SkillManager.instance.charge.swordLigtUnlocked)
        { 
            SkillManager.instance.swordLight.UseSkill(player.stat);
            AudioManager.instance.PlaySFX(86, player.transform, false);
        }

        AudioManager.instance.PlaySFX(64, player.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);

        if (triggleCalled)
            stateMachine.ChangeState(player.idleState);
    }

}