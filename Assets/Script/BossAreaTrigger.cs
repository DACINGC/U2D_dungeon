using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public Boss_Enemy boss;
    private Player player => PlayerManager.instance.player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            boss.bossAttack = true;
            AudioManager.instance.PlayBGM(6);
            AudioManager.instance.PlaySFX(12, boss.transform, false);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
