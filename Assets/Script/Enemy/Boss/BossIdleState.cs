using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{
    Boss_Enemy boss;
    public BossIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy _boss) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1.2f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (PlayerManager.instance.player.stat.isDead)
            return;

        if(stateTimer < 0 && boss.bossAttack || boss.bossAttack)
            boss.stateMachine.ChangeState(boss.chaseState);

    }
}
