using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;



public class ResistorSpawn : SpawnableItemBase
{
    public TMP_Text itemName;
    public TMP_Dropdown itemValue;
    public TMP_InputField itemQuantity;

    [SerializeField] List<GameObject> itemPrefabList = new List<GameObject>();
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
            return itemPrefabList[itemValue.value];
        }
    }

    public override void onSpawn()
    {

        Debug.Log("item Val: " + itemValue);
        Debug.Log("Spawning: " + int.Parse(itemQuantity.text) + " " + (Values)ItemValue + " " + ItemName);
        for (int i = 0; i < int.Parse(itemQuantity.text); i++)
        {
            spawnSpace += 0.25f;
            Vector3 worldSpawnLocation = new Vector3(2.4f - spawnSpace, 2.5f, 3.7f);
            Quaternion rotationValue = new Quaternion(0, 180f, 0, 0);
            var spawnObject = Instantiate(itemPrefabList[itemValue.value], worldSpawnLocation, rotationValue, workBenchSpawnedItems.transform);
            spawnObject.AddComponent<MeshRenderer>();
            spawnObject.AddComponent<BoxCollider>();
            spawnObject.GetComponent<BoxCollider>().isTrigger = true;
            spawnObject.GetComponent<BoxCollider>().size = new Vector3(0.025f,0.025f, 0.025f);
            spawnObject.gameObject.AddComponent<ResistorSelect>();
            spawnObject.tag = "Selectable";


            
            SpawnableItem resistor = new SpawnableItem(ItemName, ItemValue);
            Globals.inventoryItems.Add(resistor);

            if (itemValue.value == 0)
            {
                spawnObject.GetComponent<ResistorSelect>().resistorLabel = 210;
            }
            else if (itemValue.value == 1)
            {
                spawnObject.GetComponent<ResistorSelect>().resistorLabel = 300;
            }
            else
            {
                spawnObject.GetComponent<ResistorSelect>().resistorLabel = 470;
            }


        }
    }

    public enum Values
    {
        TwoHundredTen = 0,
        ThreeHundred = 1,
        FourHundredSeventy = 2,
    }
}
