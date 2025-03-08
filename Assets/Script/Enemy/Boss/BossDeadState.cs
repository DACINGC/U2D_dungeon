using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : EnemyState
{
    Boss_Enemy boss;
    private float destroyTime = 3;
    public BossDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy boss) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        boss.isDead = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        destroyTime -= Time.deltaTime;
        base.Update();
        if (destroyTime < 0)
        {
            boss.Destroyobj();
        }
        boss.SetVelocity(0, 0);
    }
}
