using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyState
{
    Boss_Enemy boss;

    public BossAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy _boss) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
        boss.transmitChance += 5;
        AudioManager.instance.PlaySFX(79, boss.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
        boss.lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (PlayerManager.instance.player.stat.isDead)
            stateMachine.ChangeState(boss.idleState);

        boss.SetVelocity(0, 0);

        if (triggleCalled)
        {
            if (boss.CanTransmit())
            {
                boss.stateMachine.ChangeState(boss.transmitState);
            }
            else 
            {
                boss.stateMachine.ChangeState(boss.idleState);
            }
        }
    }
}
