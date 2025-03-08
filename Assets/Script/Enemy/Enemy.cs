using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStat))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    public CapsuleCollider2D cd { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyStat stat { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public ItemDrop itemDrop { get; private set; }

    [Header("¹¥»÷¼ì²â")]
    public LayerMask playerMask;
    public Transform playerCheck;
    public float patrolDis;
    public float attackDis;

    [Header("ÒÆ¶¯")]
    public float defualtMoveSpeed;
    [HideInInspector]public float moveSpeed;

    [HideInInspector]
    public float lastAttackTime;
    public float attackCooldown;

    [Header("ËÀÍö")]
    public float desappearSpeed;

    [HideInInspector]public bool isDead;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stat = GetComponent<EnemyStat>();
        sr = GetComponentInChildren<SpriteRenderer>();
        itemDrop = GetComponent<ItemDrop>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected override void Start()
    { 
        base.Start();
        moveSpeed = defualtMoveSpeed;
    }
    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }
    public RaycastHit2D IsPlayer() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, patrolDis, playerMask);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector2(playerCheck.position.x + patrolDis * facingDir, playerCheck.position.y));
    }

    public bool CanAttack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
            return true;

        return false;
    }

    public void FreezeTime(bool _time)
    {
        if (_time)
        {
            anim.speed = 0;
            moveSpeed = 0;
        }
        else
        {
            anim.speed = 1;
            moveSpeed = defualtMoveSpeed;
        }

    }
    public void FreeseTimeDuration(float _seconds) => StartCoroutine(FreeseTimeFor(_seconds));
    public IEnumerator FreeseTimeFor(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false);
    }
    public virtual void Die()
    {
        sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * desappearSpeed));
        itemDrop.GeneralDrop();
        itemDrop.DropCash(stat.cashAmount.GetValue());
    }
}
