using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("µÙ¬‰ŒÔ∆∑")]
    [SerializeField]private Item[] prosibleDrop;
    [SerializeField]private int dropAmount;
    [SerializeField]private GameObject dropPrefab;
    private List<Item> dropList = new List<Item>();
    [SerializeField] GameObject cashPrefab;

    public void DropCash(int _amount)
    { 
        int dropAmount = Random.Range(2, _amount);
        for (int i = 0; i < dropAmount; i++)
        {
           GameObject newCash = Instantiate(cashPrefab, transform.position, Quaternion.identity);
            Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(10, 15));
            newCash.GetComponent<CashScript>().SetupVelocity(randomVelocity);
        }
    }
    public virtual void GeneralDrop()
    {

        for (int i = 0; i < prosibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) < prosibleDrop[i].dropChance * 100)
            {
                dropList.Add(prosibleDrop[i]);
            }
        }

        if (dropList.Count > 0)
            for (int i = 0; i < dropAmount; i++)
            { 
                for(int j = 0; j < dropList.Count; j++)
                {
                    DropItem(dropList[j]);
                    dropList.Remove(dropList[j]);
                }
            }
    }

    public void DropItem(Item _item)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(10, 15));
        newDrop.GetComponent<ItemScript>().SetUpItem(_item, randomVelocity);
    }


}
