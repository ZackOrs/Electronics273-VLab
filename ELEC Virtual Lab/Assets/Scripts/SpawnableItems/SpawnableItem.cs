using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableItem
{
    Globals.AvailableItems ItemName { get; }
    int ItemValue { get; }
    int ItemQuantity { get; }
    GameObject ItemPrefab { get; }
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
    public Globals.AvailableItems itemName;
    public int itemValue;
    public int itemQuantity;
    public int itemID;
    public bool isPlaced;
    public GameObject itemPrefab;

    public SpawnableItem(Globals.AvailableItems iName, int iValue)
    {
        itemName = iName;
        itemValue = iValue;
        itemID = Globals.itemIDCount++;
        isPlaced = false;
    }
    public SpawnableItem(Globals.AvailableItems iName, int iValue, bool Increment)
    {
        itemName = iName;
        itemValue = iValue;
        isPlaced = true;
    }
}