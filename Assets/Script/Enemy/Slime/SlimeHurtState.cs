using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHurtState : EnemyState
{
    Enemy_Slime slime;
    public SlimeHurtState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Enemy_Slime slime) : base(_enemyBase, _stateMachine, _aniboolName)
    { 
        this.slime = slime;
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
        base.Update();
    }
}
