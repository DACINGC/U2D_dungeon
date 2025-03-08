using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemType 
{
    public Item item;
    public int stackSize;

    //作为每一种item的类别
    public ItemType(Item _item) 
    { 
        item = _item;
        AddStack();
    }

    public void AddStack()
    {
        stackSize++;
    }
    public void RemoveStack()
    { 
        stackSize--;
    }
}
