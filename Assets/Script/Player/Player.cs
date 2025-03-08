using UnityEditor;
using UnityEngine;

public enum PlayerStates
{
    idle,
    chargeStat,
}
public class Player : Entity
{
    public PlayerStat stat { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }

    [Header("ÒÆ¶¯")]
    public float moveSpeed;
    public float jumpForce;

    [Header("³å´Ì")]
    public float dashCooldown;
    public float dashForce;
    public float dashDuration;

    [Header("¹¥»÷")]
    public float[] attackDis;
    [SerializeField] public float currentChargeTimer;
    public float chargeTime;
    
    [HideInInspector]public bool isAttacking;
    [HideInInspector]public bool isCharging;

    #region ×´Ì¬ÉùÃ÷
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSliderState wallSliderState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    public PlayerChargeState chargeState { get; private set; }
    public PlayerChargeAttackState chargeAttackState { get; private set; }
    public PlayerBlackholeState blackholeState { get; private set; }
    public PlayerFireState fireState { get; private set; }
    public PlayerDieState dieState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        stat = GetComponent<PlayerStat>();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSliderState = new PlayerWallSliderState(this, stateMachine, "WallSlider");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        chargeState = new PlayerChargeState(this, stateMachine, "Charge");
        chargeAttackState = new PlayerChargeAttackState(this, stateMachine, "ChargeAttack");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Hold");
        fireState = new PlayerFireState(this, stateMachine, "Fire");
        dieState = new PlayerDieState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialized(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        //currentChargeTimer += Time.deltaTime;
        InputController();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.instance.UseFlask();
    }

    private void InputController()
    {
        //³å´Ì
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.dashLocked && SkillManager.instance.dash.CanUseSkill() && !IsWall())
        {
            stateMachine.ChangeState(dashState);
        }
    }

    public void AnimationTriggle() => stateMachine.currentState.AnimationFinishTriggle();

    public void Die()
    {
        stateMachine.ChangeState(dieState);
    }
}
