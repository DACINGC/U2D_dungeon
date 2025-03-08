using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
    private float growSpeed;
    private float maxSize;
    private float damageTime;
    private float duration;

    public void SetUpBlackhole(float _growSpeed, float _maxSize, float _damageTime, float _duration)
    { 
        growSpeed = _growSpeed;
        maxSize = _maxSize;
        duration = _duration;
        damageTime = _damageTime;
    }

    private void Update()
    {
        duration -= Time.deltaTime;

        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        if (duration < 0)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-maxSize, -maxSize), growSpeed * Time.deltaTime);

        if (transform.localScale.x < 0.01)
            Destroy(gameObject);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (collision.GetComponent<Enemy>().isDead)
                return;

            EnemyStat enemyStat = collision.GetComponent<EnemyStat>();
            StartCoroutine(BlackholeDamge(enemyStat, damageTime));
        }
    }

    private IEnumerator BlackholeDamge(EnemyStat _targetStat, float _seconds)
    {
        _targetStat.GetComponent<Entity>().SetUpRepelDir(transform);
        PlayerManager.instance.player.stat.DoMagicDamage(_targetStat, 1);
        yield return new WaitForSeconds(_seconds);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }
}
