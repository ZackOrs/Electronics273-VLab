using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadboardBananaConnectionPanelButtons : MonoBehaviour
{
    // Start is called before the first frame update

    public Globals.BananaPlugs BananaPlugsSlotClicked = Globals.BananaPlugs.noConnection;
    public bool OptionClicked = false;

    public void WireConnectionBananaSlot0()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.B0;
        OptionClicked = true;
    }

    public void WireConnectionBananaSlot1()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.B1;
        OptionClicked = true;
    }
    public void WireConnectionBananaSlot2()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.B2;
        OptionClicked = true;
    }
    public void WireConnectionBananaSlot3()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.B3;
        OptionClicked = true;
    }
    public void WireConnectionBananaSlot4()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.B4;
        OptionClicked = true;
    }

    public void WireConnectionDisconnect()
    {
        BananaPlugsSlotClicked = Globals.BananaPlugs.noConnection;
        OptionClicked = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
