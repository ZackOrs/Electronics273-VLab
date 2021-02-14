using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemClick : MonoBehaviour
{

    public SpawnableItem Item;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(Globals.mouseClickAction == Globals.MouseClickAction.NoClick)
        {
            Debug.Log("Clicked item");
            ItemClickHandler.spawnableItem = Item;
            ItemClickHandler.buttonClicked = gameObject;
            ItemClickHandler.ItemClicked();
        }
    }
}
