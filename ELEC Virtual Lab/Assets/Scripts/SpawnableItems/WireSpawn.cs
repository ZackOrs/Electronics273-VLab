using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WireSpawn : SpawnableItemBase
{
    public TMP_Text itemName;
    public TMP_Dropdown itemValue;
    public TMP_InputField itemQuantity;
    public GameObject itemPrefab;


    [SerializeField] private GameObject workBenchSpawnedItems = null;
    public static float spawnSpace = 0.0f;

    public override Globals.AvailableItems ItemName
    {
        get
        {
            return (Globals.AvailableItems)Enum.Parse(typeof(Globals.AvailableItems), itemName.text);
        }
    }

    public override int ItemValue
    {
        get
        {
            return itemValue.value;
        }
    }

    public override int ItemQuantity
    {
        get
        {
            return int.Parse(itemQuantity.text);
        }
    }

    public override GameObject ItemPrefab
    {
        get
        {
            return itemPrefab;
        }
    }

    public override void onSpawn()
    {
        Debug.Log("Spawninggg: " + int.Parse(itemQuantity.text) + " " + (Values)ItemValue + " " + ItemName);
        for (int i = 0; i < int.Parse(itemQuantity.text); i++)
        {
            spawnSpace += 0.05f;
            Vector3 worldSpawnLocation = new Vector3(2.4f - spawnSpace, 2.5f, 3.5f);
            Quaternion rotationValue = new Quaternion(0, 0, 0, 0);
            var spawnObject = Instantiate(itemPrefab, worldSpawnLocation, rotationValue, workBenchSpawnedItems.transform);
            spawnObject.GetComponent<WireSelect>().Colour = ItemValue;
            SpawnableItem wire = new SpawnableItem(ItemName, ItemValue);
            Globals.inventoryItems.Add(wire);
        }
    }

    public enum Values
    {
        Yellow = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Black = 4,
    }
}
