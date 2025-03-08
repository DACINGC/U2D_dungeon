using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControlloer : MonoBehaviour
{
    private float moveSpeed;
    private float desappearTime;//经过一段时间会自动销毁
    private float BurstTime;
    private float shootDir;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isBurst;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    public void SetUpFire(float _moveSpeed, float _desappearTime, float _BurstTime, float _shootDir)
    {
        moveSpeed = _moveSpeed;
        desappearTime = _desappearTime;
        BurstTime = _BurstTime;
        shootDir = _shootDir;

        if (shootDir == -1)
            transform.Rotate(0, 180, 0);
    }
    private void Update()
    {
        desappearTime -= Time.deltaTime;
        BurstTime -= Time.deltaTime;

        if (BurstTime < 0)
        {
            anim.SetTrigger("Burst");
        }

        if (desappearTime < 0)
        {
            Destroy(gameObject);
        }

        if (!isBurst)
            rb.velocity = new Vector2(moveSpeed * shootDir, rb.velocity.y);    
        else
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (collision.GetComponent<Enemy>().isDead)
                return;

            isBurst = true;
            anim.SetTrigger("Burst");
        }

        if (collision.CompareTag("Ground"))
        {
            isBurst = true;
            anim.SetTrigger("Burst");
        }
    }

}
