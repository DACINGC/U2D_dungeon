using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum SwordLigtType
{
    pierce,
    burst,
}


public class SwordLightController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float speed;
    private float desapearTime;
    private int moveDir;
    private SwordLigtType type;
    private bool isBurst;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        desapearTime -= Time.deltaTime;
        if (desapearTime < 0)
        {
            Destroy(gameObject);
        }

        if(!isBurst)
            rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
        else
            rb.velocity = Vector2.zero;
    }

    public void SetupSwordLigt(float _speed, float _desapearTime, int _moveDir, SwordLigtType _type)
    { 
        speed = _speed;
        desapearTime = _desapearTime;
        moveDir = _moveDir;
        type = _type;

        if (moveDir == -1)
            transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == SwordLigtType.burst)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (collision.GetComponent<Enemy>().isDead)
                    return;

                anim.SetTrigger("Burst");
                isBurst = true;
            }

            if (collision.transform.CompareTag("Ground"))
            {
                anim.SetTrigger("Burst");
                isBurst = true;
            }
        }

        if (type == SwordLigtType.pierce)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (collision.GetComponent<Enemy>().isDead)
                    return;

                EnemyStat target = collision.GetComponent<EnemyStat>();
                collision.GetComponent<Entity>().SetUpRepelDir(transform);
                PlayerManager.instance.player.stat.DoMagicDamage(target, 15);
            }
        }
    }
    private void SwordAnimationTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().isDead)
                    return;

                EnemyStat target = hit.GetComponent<EnemyStat>();
                hit.GetComponent<Entity>().SetUpRepelDir(transform);
                PlayerManager.instance.player.stat.DoMagicDamage(target, 25);
            }
        }
    }
}
