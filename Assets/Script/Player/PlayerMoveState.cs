using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(14, player.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(14);
    }

    public override void Update()
    {
        base.Update();

        

        if (player.isAttacking)
            return;

        player.SetVelocity(player.moveSpeed * xInput, rb.velocity.y);

        if (xInput == 0 && player.IsGround())
            stateMachine.ChangeState(player.idleState);
    }
}
