using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fX { get; private set; }

    [Header("射线检测")]
    public Transform groundCheck;
    public float groundCheckDis;
    public Transform wallCheck;
    public float wallCheckDis;
    public LayerMask groundMask;

    [Header("攻击")]
    public float attackRadius;
    public Transform attackCheck;

    [Header("击退")]
    public Vector2 repelPos;
    public float repelDuration;
    private bool isRepeled;
    public int repelDir;

    [HideInInspector] public int facingDir = 1;
    protected bool facingRight = true;

    public System.Action OnFlipped;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fX = GetComponent<EntityFX>();
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }
    public void SetUpFacingDir(int _facingDir)
    { 
        facingDir = _facingDir;
        if (facingDir == -1 )
        {
            facingRight = false;
        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isRepeled)
            return;
        
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public virtual void Flip()
    {
        facingRight = !facingRight;
        facingDir *= -1;
        transform.Rotate(0, 180, 0);

        if (OnFlipped != null)
            OnFlipped();
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    public void DamageEffect()
    {
        StartCoroutine(fX.HitFx());
        StartCoroutine(Repel());
    }
    //设置受击方向
    public void SetUpRepelDir(Transform _transform)
    {
        if (_transform.position.x > transform.position.x)
            repelDir = -1;
        else if (_transform.position.x < transform.position.x)
            repelDir = 1;
    }
    //受击击退效果
    public IEnumerator Repel()
    {
        isRepeled = true;
        rb.velocity = new Vector2(repelPos.x * repelDir, repelPos.y);
        yield return new WaitForSeconds(repelDuration);
        isRepeled = false;  
    }
    public bool IsGround() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, groundMask);
    public bool IsWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDis, groundMask);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDis * facingDir, wallCheck.position.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }

    public void Destroyobj()
    { 
        Destroy(gameObject);
    }
    
}
