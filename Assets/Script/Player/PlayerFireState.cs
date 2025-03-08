using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireState : PlayerState
{

    public PlayerFireState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(89, player.transform, false);
        
        SkillManager.instance.fire.UseSkill(player.stat);
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
