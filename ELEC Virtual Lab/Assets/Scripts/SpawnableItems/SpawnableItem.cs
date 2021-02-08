using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableItem
{    
    Globals.availableItems ItemName {get;}
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
    public Globals.availableItems itemName;
    public int itemValue;
    public int itemQuantity;
    public int itemID;
    public GameObject itemPrefab;

    public SpawnableItem(Globals.availableItems iName, int iValue)
    {
         itemName = iName;
         itemValue = iValue;
         itemID = Globals.itemIDCount++;
    }
}