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
            Vector3 worldSpawnLocation = new Vector3(2.4f - spawnSpace, 2.67f, 3.9f);
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
            float capacitorVal = 0;
            switch (itemValue.value)
            {
                case (0):
                    capacitorVal = 0.0000001f;
                    break;

                case (1):
                    capacitorVal = 0.000001f;
                    break;

                case (2):
                    capacitorVal = 0.000004f;
                    break;
                case (3):
                    capacitorVal = 0.000005f;
                    break;

                case (4):
                    capacitorVal = 0.000009f;
                    break;

                case (5):
                    capacitorVal = 0.000012f;
                    break;
                case (6):
                    capacitorVal = 0.000015f;
                    break;

                case (7):
                    capacitorVal = 0.000019f;
                    break;

                case (8):
                    capacitorVal = 0.000020f;
                    break;
                default:
                    break;
            }
            spawnObject.GetComponent<CapacitorSelect>().CapacitorValue = capacitorVal; 
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
