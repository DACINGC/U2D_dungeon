using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashScript : MonoBehaviour
{
    private float desapearTime = 6;
    [SerializeField] private Rigidbody2D rb;
    public int cash;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetupVelocity(Vector2 _velocity)
    {
        rb.velocity = _velocity;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position) < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.player.transform.position, 8 * Time.deltaTime);
        }

        desapearTime -= Time.deltaTime;
        if (desapearTime < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
            PlayerManager.instance.currentCash += cash;
        }
    }
}
