using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : EnemyState
{
    Transform player;
    Boss_Enemy boss;

    public BossChaseState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy _boss) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 3;
        player = PlayerManager.instance.player.transform;

    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(80);
    }

    public override void Update()
    {
        base.Update();
        //AudioManager.instance.PlaySFX(80, boss.transform, false);

        if (PlayerManager.instance.player.stat.isDead)
            stateMachine.ChangeState(boss.idleState);

        if (boss.bossAttack == false)
            stateMachine.ChangeState(boss.idleState);

        if (player.position.x > boss.transform.position.x)
            boss.facingDir = 1;
        else if (player.position.x < boss.transform.position.x)
            boss.facingDir = -1;

        if (boss.IsPlayer())
            if (boss.IsPlayer().distance < boss.attackDis && boss.CanAttack())
                stateMachine.ChangeState(boss.attackState);

        //if (Mathf.Abs(slime.IsPlayer().distance) < 1)
        //    return;

        //与玩家之间的距离
        if (Mathf.Abs(boss.transform.position.x - player.transform.position.x) < 1)
            boss.SetVelocity(0, 0);
        else
            boss.SetVelocity(boss.moveSpeed * boss.facingDir, rb.velocity.y);
    }


}
