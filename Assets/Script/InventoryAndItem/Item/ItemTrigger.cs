using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private ItemScript script => GetComponentInParent<ItemScript>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<CharacterStat>().isDead)
                return;

            Debug.Log("Pick Up");
            script.PickUpItem();
        }
    }
}
