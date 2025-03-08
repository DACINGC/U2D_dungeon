using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(42, player.transform, false);
        stateTimer = 1f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsWall())
            stateMachine.ChangeState(player.wallSliderState);

        if (player.IsGround())
            stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.fallState);

    }
}
