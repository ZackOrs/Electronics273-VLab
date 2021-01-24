using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{

    public SpawnableItem spawnableItem;
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemClicked()
    {
        Debug.Log("Item component is: " + spawnableItem.itemName.ToString());
        ItemClickToHandle();
    }

    private void ItemClickToHandle()
    {
        
        switch(spawnableItem.itemName)
        {
            case Globals.availableItems.Wire:
                WireClicked();
                break;
            case Globals.availableItems.Resistor:
                ResistorClicked();
                break;
            case Globals.availableItems.Capacitor:
                CapacitorClicked();
                break;
            default:
                Debug.Log("Click not recognized");
                break;
        }
    }

    private void WireClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }
        private void ResistorClicked()
    {
         Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }
        private void CapacitorClicked()
    {
         Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }
}
