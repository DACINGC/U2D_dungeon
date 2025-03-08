using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossConjureState : EnemyState
{
    Boss_Enemy boss;
    private int amountOfConjure;

    private float conjureTimer;
    public BossConjureState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Boss_Enemy boos) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.boss = boos;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfConjure = boss.amountOfConjure;
        conjureTimer = 0.5f;
        AudioManager.instance.PlaySFX(6, boss.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (PlayerManager.instance.player.stat.isDead)
            stateMachine.ChangeState(boss.idleState);

        conjureTimer -= Time.deltaTime;

        if (CanConjure())
        {
            boss.CreateConjure();
        }
        

        if (amountOfConjure <= 0)
            stateMachine.ChangeState(boss.transmitState);

    }

    private bool CanConjure()
    {
        if (amountOfConjure > 0 && conjureTimer < 0)
        {
            conjureTimer = boss.conjureCooldown;
            amountOfConjure--;
            return true;
        }
        return false;
    }
}
