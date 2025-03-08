using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _aniBoolName) : base(_player, _stateMachine, _aniBoolName)
    {
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

        if (!player.IsGround())
            stateMachine.ChangeState(player.fallState);

        //射击技能
        if (Input.GetKeyDown(KeyCode.F) && SkillManager.instance.fire.fireLocked && SkillManager.instance.fire.CanUseSkill())
            stateMachine.ChangeState(player.fireState);

        //黑洞技能
        if (Input.GetKeyDown(KeyCode.R) && SkillManager.instance.blackhole.blackholeLocked && SkillManager.instance.blackhole.CanUseSkill())
            stateMachine.ChangeState(player.blackholeState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (Input.GetKeyDown(KeyCode.Mouse1) && SkillManager.instance.charge.chargeUnlocked)
            stateMachine.ChangeState(player.chargeState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGround())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

}
