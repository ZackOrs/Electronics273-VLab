using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCreationManager : MonoBehaviour
{
    [SerializeField] GameObject parentWireConnections = null;
    [SerializeField] List<GameObject> bananaSlotLocationList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateWireStartPointEndPoint(Transform startPoint, Globals.BananaPlugs endPointBananaPlugConnection)
    {
        //Fix the colour
        //Make the tips thicker so it looks like banana plugs
        //Remove all connections on Disconnect
        GameObject line = new GameObject(startPoint.name.ToString() + "/" + endPointBananaPlugConnection.ToString());
        line.transform.SetParent(parentWireConnections.transform);
        line.AddComponent<LineRenderer>();
        line.GetComponent<LineRenderer>().SetPosition(0, startPoint.position);
        line.GetComponent<LineRenderer>().SetPosition(1,GetBananaSlotObject(endPointBananaPlugConnection).position);
        line.GetComponent<LineRenderer>().startWidth = 0.004f;
        line.GetComponent<LineRenderer>().endWidth = 0.004f;
        line.GetComponent<LineRenderer>().startColor = Color.green;
        line.GetComponent<LineRenderer>().endColor = Color.green;
    }


    private Transform GetBananaSlotObject(Globals.BananaPlugs endPointBananaPlugConnection)
    {
        switch (endPointBananaPlugConnection)
        {
            //Add the rest of the cases
            case (Globals.BananaPlugs.B0):
            return bananaSlotLocationList[0].transform;
            case (Globals.BananaPlugs.B1):
            return bananaSlotLocationList[1].transform;
            case (Globals.BananaPlugs.B2):
            return bananaSlotLocationList[2].transform;
            default:
            //Make it connect to the original location (Voltageinput to VoltageInput)
            RemoveAllConnections();
            return null;    //Fix the null crash
        }
    }

    private void RemoveAllConnections()
    {
        //removes all connections to the attached socket in machine
        //go through all children of parentWireConnections
        //split the name on "/"
        //if the start begins with the startPoint.name.toString()
        //remove the object
    }
}