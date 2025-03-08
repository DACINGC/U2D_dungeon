using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stat.MakeInvincible(true);
        AudioManager.instance.PlaySFX(61, player.transform, false);
        stateTimer = player.dashDuration;

        
    }

    public override void Exit()
    {
        base.Exit();
        player.stat.MakeInvincible(false);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        player.GetComponent<EntityFX>().CreateDashClone();

        rb.velocity = new Vector2(player.facingDir * player.dashForce, 0);
        if ( stateTimer< 0) 
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.IsWall() && !player.IsGround())
            stateMachine.ChangeState(player.wallSliderState);
    }

}
