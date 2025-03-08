using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime : Enemy
{
    #region ×´Ì¬ÉùÃ÷
    public SlimeIdleState idleState { get; private set; }
    public SlimePatrolState patrolState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeChashState chashState { get; private set; }
    public SlimeDieState dieState { get; private set; }
    public SlimeHurtState hurtState { get; private set; }
    #endregion

    protected override void Awake()
    {
        
        SetUpFacingDir(-1);
        base.Awake();
        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        patrolState = new SlimePatrolState(this, stateMachine, "Move", this);
        attackState = new SlimeAttackState(this, stateMachine, "Attack", this);
        chashState = new SlimeChashState(this, stateMachine, "Move", this);
        dieState = new SlimeDieState(this, stateMachine, "Die", this);
        hurtState = new SlimeHurtState(this, stateMachine, "Hurt", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(dieState);
    }
}
