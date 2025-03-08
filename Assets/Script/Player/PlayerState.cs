using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    private string aniBoolName;

    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected float xInputDir;

    protected bool triggleCalled;//用于动画的停止

    protected float stateTimer;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine,string _aniBoolName)
    { 
        player = _player;
        stateMachine = _stateMachine;
        aniBoolName = _aniBoolName;
    }
    public virtual void Enter()
    {
        //Debug.Log("进入：" + aniBoolName);
        triggleCalled = false;
        player.anim.SetBool(aniBoolName, true);
        rb = player.rb;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        player.anim.SetFloat("yVelocity", rb.velocity.y);

    }


    public virtual void Exit()
    {
        player.anim.SetBool(aniBoolName, false);
    }

    public void AnimationFinishTriggle()
    {
        triggleCalled = true;
    }
}
