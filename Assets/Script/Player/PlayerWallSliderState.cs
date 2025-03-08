using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSliderState : PlayerState
{
    private int wallDir;
    public PlayerWallSliderState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
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
        //判断输入是否与面朝方向一致
        if (xInput > 0)
            wallDir = 1;
        else if (xInput < 0)
            wallDir = -1;

        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            stateMachine.ChangeState(player.wallJumpState);//如果walljump为子状态的话，依然会执行下列程序
            return;
        }

        if (xInput != 0 && wallDir != player.facingDir)
            stateMachine.ChangeState(player.idleState);

        //判断y方向是否有输入，不然减少下降的速度
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);

        if (player.IsGround() || !player.IsWall())
            stateMachine.ChangeState(player.idleState);
    }

}
