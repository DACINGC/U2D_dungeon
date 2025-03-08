using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatrolState : EnemyState
{
    Enemy_Slime slime;
    public SlimePatrolState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName ,Enemy_Slime _enemy) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        slime = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        stateTimer = 6f;
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(80);
    }

    public override void Update()
    {
        base.Update();
        AudioManager.instance.PlaySFX(80, slime.transform, false);
        slime.SetVelocity(slime.moveSpeed * slime.facingDir, rb.velocity.y);

        if (slime.IsWall() || !slime.IsGround())
        { 
            slime.Flip();
            stateMachine.ChangeState(slime.idleState);
        }

        if (slime.IsPlayer() && !PlayerManager.instance.player.stat.isDead)
           stateMachine.ChangeState(slime.chashState);

    }
}
