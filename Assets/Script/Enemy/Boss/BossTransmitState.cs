using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransmitState : EnemyState
{
    Boss_Enemy boss;

    public BossTransmitState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy boss) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(21, boss.transform, false);
        boss.stat.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();
        boss.stat.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        if (triggleCalled)
        {
            if (boss.CanCanjure())
            {
                boss.stateMachine.ChangeState(boss.conjureState);
            }
            else
            {
                boss.stateMachine.ChangeState(boss.chaseState);
            }
        }

    }
}
