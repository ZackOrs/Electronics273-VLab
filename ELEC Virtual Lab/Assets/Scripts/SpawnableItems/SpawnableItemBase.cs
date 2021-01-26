using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableItemBase : MonoBehaviour , ISpawnableItem
{
    public virtual Globals.AvailableItems ItemName
    {
        get
        {
            return 0;
        }
    }
    public virtual int ItemValue
    {
        get
        {
            return 0;
        }
    }
    public virtual int ItemQuantity
    {
        get
        {
            return 0;
        }
    }
    public virtual GameObject ItemPrefab
    {
        get
        {
            return null;
        }
    }

    public virtual void onSpawn()
    {

    }

}
