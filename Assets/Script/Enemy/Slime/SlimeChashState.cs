using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChashState : EnemyState
{
    Transform player;
    Enemy_Slime slime;


    public SlimeChashState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName,Enemy_Slime _slime) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        slime = _slime;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 3;
        player = PlayerManager.instance.player.transform;

        slime.anim.speed = 1.2f;
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(80);
        slime.anim.speed = 1;
    }

    public override void Update()
    {
        base.Update();
        AudioManager.instance.PlaySFX(80, slime.transform, false);

        if (player.position.x > slime.transform.position.x)
            slime.facingDir = 1;
        else if (player.position.x < slime.transform.position.x)
            slime.facingDir = -1;

        if (!slime.IsPlayer() && stateTimer < 0 || PlayerManager.instance.player.stat.isDead)
            stateMachine.ChangeState(slime.patrolState);

        if (slime.IsPlayer())
            if (slime.IsPlayer().distance < slime.attackDis && slime.CanAttack())
                stateMachine.ChangeState(slime.attackState);

        //if (Mathf.Abs(slime.IsPlayer().distance) < 1)
        //    return;

        if (Mathf.Abs(slime.transform.position.x - player.transform.position.x) < 1)
            slime.SetVelocity(0, 0);
        else
            slime.SetVelocity(slime.moveSpeed * 1.2f * slime.facingDir, rb.velocity.y);
    }


}
