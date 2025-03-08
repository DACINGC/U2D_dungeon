using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enemy : Enemy
{
    #region 状态声明
    public BossChaseState chaseState { get; private set; }
    public BossAttackState attackState { get; private set; }
    public BossConjureState conjureState { get; private set;}
    public BossIdleState idleState { get; private set; }
    public BossTransmitState transmitState { get; private set; }
    public BossDeadState deadState { get; private set; }
    #endregion

    [Header("传送")]
    [SerializeField] private BoxCollider2D area;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float transmitChance;
    [SerializeField] private float defaultTransmitChance;
    [Header("施法")]
    [SerializeField] private GameObject conjurePrefab;
    [SerializeField] private float conjureStateCooldown;
    public int amountOfConjure;
    public float conjureCooldown;
    public bool bossAttack;

    private float lastConjureTime;


    protected override void Awake()
    {
        base.Awake();
        chaseState = new BossChaseState(this, stateMachine, "Move", this);
        attackState = new BossAttackState(this, stateMachine, "Attack", this);
        conjureState = new BossConjureState(this, stateMachine, "Conjure", this);
        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        transmitState = new BossTransmitState(this, stateMachine, "Exit", this);
        deadState = new BossDeadState(this, stateMachine, "Die", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        SetUpFacingDir(-1);
        transmitChance = defaultTransmitChance;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void FindPosition()
    {
        float x = Random.Range(area.bounds.min.x + 3, area.bounds.max.x - 3);
        float y = Random.Range(area.bounds.min.y + 3, area.bounds.max.y - 3);
        transform.position = new Vector3(x, y, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y) / 2);

        if(!GroundBelow() || SomethingIsAround())
        {
            Debug.Log("Tranmit wrong");
            FindPosition();
        }
    }

    //检测与地面的距离
    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, groundMask);
    //检测传送的地方是否时墙体内
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, groundMask);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        //离地面的距离
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.x - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    //可以传送
    public bool CanTransmit()
    {
        if (Random.Range(0, 100) < transmitChance)
        {
            transmitChance = defaultTransmitChance;
            return true;
        }
        else
            return false;
    }

    public void CreateConjure()
    {
        Player player = PlayerManager.instance.player;

        Vector3 conjurePosition = new Vector3(player.transform.position.x + player.facingDir * 0.3f, player.transform.position.y + 1, 0);

        GameObject newConjure = Instantiate(conjurePrefab, conjurePosition, Quaternion.identity);

        newConjure.GetComponent<ConjuerController>().SetUpConjure(stat);
    }
    public bool CanCanjure()
    {
        if (Time.time > lastConjureTime + conjureStateCooldown)
        {
            lastConjureTime = Time.time;
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
