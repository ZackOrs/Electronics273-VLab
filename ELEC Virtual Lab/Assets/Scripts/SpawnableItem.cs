using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableItem
{    
    string ItemName {get;}
    int ItemValue {get;}
    int ItemQuantity{get;}
    GameObject ItemPrefab {get;}
    void onSpawn();

}

public class SpawnableItemsEventArgs : EventArgs
{

    public SpawnableItemsEventArgs(ISpawnableItem item)
    {
        Item = item;
    }

    public ISpawnableItem Item;

}

public class SpawnableItem
{
    public string ItemName;
    public int ItemValue;
    public int ItemQuantity;
    public GameObject ItemPrefab;

    public SpawnableItem(string itemName, int itemValue)
    {
         ItemName = itemName;
         ItemValue = itemValue;
    }
}