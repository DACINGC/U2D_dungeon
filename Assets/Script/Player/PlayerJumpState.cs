using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerGroundState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(41, player.transform, false);

        player.SetVelocity(rb.velocity.x, player.jumpForce);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }
}
