using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectableItem
{    
    string Name {get;}
    void onInteract();
}

public class SelectableItemsEventArgs : EventArgs
{

    public SelectableItemsEventArgs(ISelectableItem item)
    {
        Item = item;
    }

    public ISelectableItem Item;

}

