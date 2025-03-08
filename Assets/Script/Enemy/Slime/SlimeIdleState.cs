using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState: EnemyState
{
    protected Enemy_Slime slime;
    public SlimeIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Enemy_Slime _enemy) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        slime = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(slime.patrolState);
        }
    }
}
