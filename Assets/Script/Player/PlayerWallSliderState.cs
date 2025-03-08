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
        //�ж������Ƿ����泯����һ��
        if (xInput > 0)
            wallDir = 1;
        else if (xInput < 0)
            wallDir = -1;

        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            stateMachine.ChangeState(player.wallJumpState);//���walljumpΪ��״̬�Ļ�����Ȼ��ִ�����г���
            return;
        }

        if (xInput != 0 && wallDir != player.facingDir)
            stateMachine.ChangeState(player.idleState);

        //�ж�y�����Ƿ������룬��Ȼ�����½����ٶ�
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);

        if (player.IsGround() || !player.IsWall())
            stateMachine.ChangeState(player.idleState);
    }

}
