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
        Debug.Log("name: " + ItemName);
        Debug.Log("val: " + (Values)ItemValue);
        Debug.Log("Quant: " + ItemQuantity + "\n");

        Instantiate(itemPrefab);
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
