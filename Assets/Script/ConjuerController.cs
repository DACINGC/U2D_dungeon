using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuerController : MonoBehaviour
{
    [SerializeField] private Transform boxCheck;
    [SerializeField] private Vector2 boxSize;

    private CharacterStat stat;
    private void OnEnable()
    {
        AudioManager.instance.PlaySFX(87,transform, false);
    }
    public void SetUpConjure(CharacterStat myStat)
    {
        stat = myStat;
    }
    private void AnimationTrigger()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCheck.position, boxSize, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Entity>().DamageEffect();
                stat.DoMagicDamage(hit.GetComponent<CharacterStat>(), 10);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(boxCheck.position, boxSize);
    }

    private void DestroyGameObject()
    { 
        Destroy(gameObject);
    }
}
