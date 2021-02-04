using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItemBase : MonoBehaviour , ISelectableItem
{
    public virtual string Name
    {
        get
        {
            return "";
        }
    }

    public virtual void onInteract()
    {

    }

}
