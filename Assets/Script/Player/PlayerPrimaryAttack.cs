using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    public int attackNum {get; private set;}
    private float lastAttackTime;
    private float attackWindow = 1.5f;

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(Random.Range(38, 41), player.transform, false);
        player.isAttacking = true;

        if (attackNum > 2 || Time.time > lastAttackTime + attackWindow)
            attackNum = 0;

        rb.velocity = new Vector2(player.attackDis[attackNum] * player.facingDir, rb.velocity.y);

        player.anim.SetInteger("AttackNum", attackNum);

    }

    public override void Exit()
    {
        base.Exit();
        attackNum++;
        lastAttackTime = Time.time;
        player.isAttacking = false;
    }

    public override void Update()
    {
        base.Update();

        if (triggleCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
