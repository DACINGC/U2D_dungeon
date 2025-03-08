using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.blackhole.UseSkill(player.stat);
        AudioManager.instance.PlaySFX(84, player.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);

        if (SkillManager.instance.blackhole.BlackHoleFinished())
            stateMachine.ChangeState(player.idleState);
    }

}
