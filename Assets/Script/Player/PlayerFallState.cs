using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);

        if (player.IsWall())
            stateMachine.ChangeState(player.wallSliderState);

        if (player.IsGround())
            stateMachine.ChangeState(player.idleState);
    }
}
