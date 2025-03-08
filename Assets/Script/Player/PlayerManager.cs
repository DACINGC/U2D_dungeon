using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;
    public int currentCash;
    public UI ui;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    public bool HanveEnoughCash(int _price)
    {
        if (_price > currentCash)
        {
            Debug.Log("没有足够的钱来解锁技能了");
            return false;
        }

        currentCash -= _price;
        return true;
    }

    public void LoadData(GameData _data)
    {
        currentCash = _data.currence;
    }
    public int GetCurrentCash()
    { 
        return currentCash;
    }
    public void SaveData(ref GameData _data)
    {
        _data.currence = currentCash;
    }
}
