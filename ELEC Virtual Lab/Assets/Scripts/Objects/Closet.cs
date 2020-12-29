using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : InteractableItemBase
{
    public override void onInteract()
    {
        InteractText = "Press F to open closet";
    }


}
 