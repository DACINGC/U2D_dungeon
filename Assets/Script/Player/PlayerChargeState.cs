using UnityEngine;

public class PlayerChargeState : PlayerGroundState
{
    public PlayerChargeState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isCharging = true;
        stateTimer = 1;
    }

    public override void Exit()
    {
        base.Exit();
        player.currentChargeTimer = 0;
        player.isCharging = false;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        player.currentChargeTimer = stateTimer;

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (stateTimer < (1- player.chargeTime))
                stateMachine.ChangeState(player.chargeAttackState);
            else
                stateMachine.ChangeState(player.idleState);
        }


    }
}
