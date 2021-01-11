using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WireSpawn : SpawnableItemBase
{
    public TMP_Text itemName;
    public TMP_Dropdown itemValue;
    public TMP_InputField itemQuantity;
    public GameObject itemPrefab;

    [SerializeField] private GameObject WorkBench;
    private static float spawnSpace = 0.0f;

    public override string ItemName
    {
        get
        {
            return itemName.text;
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
        Debug.Log("Spawning: "+ItemQuantity+" " +(Values)ItemValue + " " +ItemName);
        for(int i = 0 ; i < ItemQuantity ; i++)
        {
            spawnSpace +=0.05f;
            Vector3 abc = new Vector3(2.4f - spawnSpace,2.5f,3.5f);
            Quaternion xyz = new Quaternion(0,0,0,0);
            var spawnObject = Instantiate(itemPrefab,abc,xyz, WorkBench.transform);
            spawnObject.GetComponent<WireSelect>().Colour = ItemValue;
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
