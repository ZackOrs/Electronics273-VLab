using UnityEngine;
using System.Collections.Generic;

public class WireCreationManager : MonoBehaviour
{
    [SerializeField] GameObject parentWireConnections = null;
    [SerializeField] List<GameObject> bananaSlotLocationList = new List<GameObject>();
    public Color highlightMaterial;
    public static float dist = 0.0f;
    private float counter;
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
        RemoveAllConnections(startPoint.name);
        
        if(endPointBananaPlugConnection != Globals.BananaPlugs.noConnection)
        {
            //Fix the colour
            //Make the tips thicker so it looks like banana plugs
            //Remove all connections on Disconnect
            GameObject line = new GameObject(startPoint.name.ToString() + "/" + endPointBananaPlugConnection.ToString());
            line.transform.SetParent(parentWireConnections.transform);
            line.AddComponent<LineRenderer>();
            line.GetComponent<LineRenderer>().SetPosition(0, startPoint.position);
            line.GetComponent<LineRenderer>().SetPosition(1, GetBananaSlotObject(endPointBananaPlugConnection, startPoint).position);
            line.GetComponent<LineRenderer>().startWidth = 0.008f;
            line.GetComponent<LineRenderer>().endWidth = 0.008f;
            line.GetComponent<LineRenderer>().material.color = highlightMaterial;

            //LineRenderer startingline = new LineRenderer();
            //LineRenderer endingline = new LineRenderer();

            Vector3 enddist = new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z - 0.05f);
            Vector3[] startPosition = new Vector3[2] { startPoint.position, enddist };

            Vector3 startdist = new Vector3(GetBananaSlotObject(endPointBananaPlugConnection, startPoint).position.x, GetBananaSlotObject(endPointBananaPlugConnection, startPoint).position.y + 0.1f, GetBananaSlotObject(endPointBananaPlugConnection, startPoint).position.z);
            Vector3[] endPosition = new Vector3[2] { GetBananaSlotObject(endPointBananaPlugConnection, startPoint).position, startdist };

            DrawObjectLine(startPosition, startPoint);
            DrawObjectLine(endPosition, startPoint);
        }
  

    }

    void DrawObjectLine(Vector3[] vertexPositions, Transform start)
    {
        //GameObject lineRenderer = new GameObject();
        GameObject cylinder = new GameObject(start.name + "/plug");
        cylinder.transform.SetParent(parentWireConnections.transform);
        cylinder.AddComponent<LineRenderer>();
        cylinder.GetComponent<LineRenderer>().positionCount = 2;
        cylinder.GetComponent<LineRenderer>().SetPositions(vertexPositions);
        cylinder.GetComponent<LineRenderer>().startWidth = 0.05f;
        cylinder.GetComponent<LineRenderer>().endWidth = 0.05f;
        cylinder.GetComponent<LineRenderer>().material.color = highlightMaterial;
    }

    private Transform GetBananaSlotObject(Globals.BananaPlugs endPointBananaPlugConnection, Transform startLocation)
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
            case (Globals.BananaPlugs.B3):
                return bananaSlotLocationList[3].transform;
            case (Globals.BananaPlugs.B4):
                return bananaSlotLocationList[4].transform;
            case (Globals.BananaPlugs.noConnection):
                RemoveAllConnections(startLocation.name);
                return startLocation;
            default:
                return startLocation;
        }
    }

    private void RemoveAllConnections(string itemToRemove)
    {
        //removes all connections to the attached socket in machine
        //go through all children of parentWireConnections
        //split the name on "/"
        //if the start begins with the startPoint.name.toString()
        //remove the objectt
        //compareitemtoremove to split first part
        for (int i = 0; i < parentWireConnections.transform.childCount; i++)
        {
            var child = parentWireConnections.transform.GetChild(i);

            string[] array = child.name.Split('/');

            if (itemToRemove == array[0])
            {
                Destroy(child.gameObject);
            }
        }
        //parentWireConnections.transform.childCount;
    }
}
