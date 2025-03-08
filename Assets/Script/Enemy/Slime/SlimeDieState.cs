using UnityEngine;

public class SlimeDieState : EnemyState
{
    Enemy_Slime slime;
    private float destroyTime = 3;
    public SlimeDieState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _aniboolName, Enemy_Slime slime) : base(_enemyBase, _stateMachine, _aniboolName)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        slime.isDead = true;
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
            slime.Destroyobj();
        }
        slime.SetVelocity(0, 0);
    }

}
