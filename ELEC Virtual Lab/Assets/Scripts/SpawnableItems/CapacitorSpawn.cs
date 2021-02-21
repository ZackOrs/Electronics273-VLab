using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CapacitorSpawn : SpawnableItemBase
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
        Debug.Log("Spawning: " + int.Parse(itemQuantity.text) + " " + (Values)ItemValue + " " + ItemName);
        for (int i = 0; i < int.Parse(itemQuantity.text); i++)
        {
            spawnSpace += 0.20f;
            Vector3 worldSpawnLocation = new Vector3(2.4f - spawnSpace, 2.6f, 3.9f);
            Quaternion rotationValue = new Quaternion(0, 0, 0, 0);
            var spawnObject = Instantiate(itemPrefab, worldSpawnLocation, rotationValue, workBenchSpawnedItems.transform);
            spawnObject.gameObject.AddComponent<CapacitorSelect>();
            spawnObject.AddComponent<MeshRenderer>();
            spawnObject.AddComponent<BoxCollider>();
            spawnObject.GetComponent<BoxCollider>().isTrigger = true;
            spawnObject.GetComponent<BoxCollider>().size = new Vector3(0.025f, 0.025f, 0.025f);
            spawnObject.tag = "Selectable";

            
            SpawnableItem capacitor = new SpawnableItem(ItemName, ItemValue);
            Globals.inventoryItems.Add(capacitor);

            
            if (itemValue.value == 0)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.000001f;
            }
            else if (itemValue.value == 1)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00001f;
            }
            else if (itemValue.value == 2)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00004f;
            }
            else if (itemValue.value == 3)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00005f;
            }
            else if (itemValue.value == 4)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00009f;
            }
            else if (itemValue.value == 5)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00012f;
            }
            else if (itemValue.value == 6)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00015f;
            }
            else if (itemValue.value == 7)
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00019f;
            }
            else
            {
                spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = 0.00020f;
            }
        }
    }

    public enum Values
    {
        ZeroPointOne = 0,
        One = 1,
        Four = 2,
        Five = 3,
        Nine = 4,
        Twelve = 5,
        Fifteen = 6,
        Nineteen = 7,
        Twenty = 8,
    }
}
