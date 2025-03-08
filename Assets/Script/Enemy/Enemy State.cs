using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public Enemy enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public string aniBoolName { get; private set; }

    protected float stateTimer;

    protected Rigidbody2D rb;

    protected bool triggleCalled;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName)
    { 
        enemy = _enemyBase;
        stateMachine = _stateMachine;
        aniBoolName = _aniboolName;
    }

    public virtual void Enter()
    { 
        enemy.anim.SetBool(aniBoolName, true);
        triggleCalled = false;
    }
    public virtual void Update()
    { 
        stateTimer -= Time.deltaTime;
        rb = enemy.rb;
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(aniBoolName, false);
    }

    public void AnimationFinishTriggle()
    { 
        triggleCalled = true;
    }
}
