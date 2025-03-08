using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    Enemy_Slime slime;

    public SlimeAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName,Enemy_Slime _slime) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(79, slime.transform, false);
    }

    public override void Exit()
    {
        base.Exit();
        slime.lastAttackTime = Time.time;
}

    public override void Update()
    {
        base.Update();

        slime.SetVelocity(0, 0);

        if (triggleCalled)
            stateMachine.ChangeState(slime.chashState);
    }
}
