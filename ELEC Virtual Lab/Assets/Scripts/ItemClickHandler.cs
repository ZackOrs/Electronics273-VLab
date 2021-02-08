using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public static SpawnableItem spawnableItem;

    [SerializeField] GameObject _breadboardUI;
    [SerializeField] Image _wireImage;

    public static bool isBBSlotFree = false;
    private GameObject _pointA = null;
    private GameObject _pointB = null;

    
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.mouseClickAction > 0)
        {
            WaitForClick();
        }
    }

    public static void ItemClicked()
    {
        if (Globals.mouseClickAction == 0 && spawnableItem!=null)
        {
             ItemClickToHandle();
        }
    }

    private static void ItemClickToHandle()
    {
        switch (spawnableItem.itemName)
        {
            case Globals.AvailableItems.Wire:
                WireClicked();
                break;
            case Globals.AvailableItems.Resistor:
                ResistorClicked();
                break;
            case Globals.AvailableItems.Capacitor:
                CapacitorClicked();
                break;
            default:
                Debug.Log("Click not recognized");
                break;
        }
    }

    private static void WireClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
        Globals.mouseClickAction = Globals.MouseClickAction.TwoClicks_FirstClick;
    }

    private static void ResistorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }

    private static void CapacitorClicked()
    {
        Debug.Log("I clicked: " + spawnableItem.itemValue + " " + spawnableItem.itemName + " with ID: " + spawnableItem.itemID);
    }

    private void WaitForClick()
    {
        switch (Globals.mouseClickAction)
        {
            case Globals.MouseClickAction.TwoClicks_FirstClick:
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlotFree)
                    {
                        Debug.Log("First Click GOOD");
                    }
                }
                break;

            case Globals.MouseClickAction.TwoClicks_SecondClick:
                if (Input.GetMouseButtonDown(0))
                {
                    CheckIfBBSlot();
                    if (isBBSlotFree)
                    {
                        Debug.Log("Second Click GOOD DRAWING LINE");
                        DrawLineBetweenPoints();
                    }
                }
                break;

            default:
                Debug.Log("Something is wrong");
                break;
        }
    }

    private void DrawLineBetweenPoints()
    {  
        ResetWire();
        // Instantiate(_wireImage,_breadboardUI.transform);

        float distance = Vector2.Distance(_pointA.transform.position,_pointB.transform.position);
        float rotation = AngleBetweenVector2(_pointA.transform.position,_pointB.transform.position);
        float posX = PositionXForLine(_pointA.transform.position,_pointB.transform.position);
        float posY = PositionYForLine(_pointA.transform.position,_pointB.transform.position);


        Debug.Log("Rotation is:" + rotation);

        Debug.Log("X is:" + posX);
        Debug.Log("Y is:" + posY);
        _wireImage.transform.position = new Vector2(posX,posY);
        _wireImage.rectTransform.sizeDelta = new Vector2(distance, _wireImage.rectTransform.sizeDelta.y);
        _wireImage.transform.Rotate(0,0,rotation,Space.World);

        
        Instantiate(_wireImage,_breadboardUI.transform);

        // //For creating line renderer object
        // LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        // lineRenderer.transform.parent = _breadboardUI.transform;
        // lineRenderer.material = _lineMaterial;    
        // lineRenderer.startColor = Color.black;
        // lineRenderer.endColor = Color.black;
        // lineRenderer.startWidth = 5.0f;
        // lineRenderer.endWidth = 5.0f;
        // lineRenderer.positionCount = 2;
        // lineRenderer.useWorldSpace = true;    

        // //For drawing line in the world space, provide the x,y,z values
        // lineRenderer.SetPosition(0, _pointA.transform.position); //x,y and z position of the starting point of the line
        // lineRenderer.SetPosition(1, _pointB.transform.position); //x,y and z position of the starting point of the line

    }

    private void ResetWire()
    {
        _wireImage.transform.position = new Vector3(0,0,0);
        _wireImage.transform.Rotate(0,0,0,Space.World);
        _wireImage.rectTransform.sizeDelta = new Vector2(25, _wireImage.rectTransform.sizeDelta.y);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
            Vector2 diference = vec2 - vec1;
            float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
            return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private float PositionXForLine(Vector2 vec1, Vector2 vec2)
    {



        float x1 = vec1.x;
        float x2 = vec2.x;
        Debug.Log("X1 is:" + x1);
        Debug.Log("X2 is:" + x2);
        if(x1 < x2)
            return x1;
        else
            return x2;

    }

    private float PositionYForLine(Vector2 vec1, Vector2 vec2)
    {
        float y1 = vec1.y - 500;
        float y2 = vec2.y - 500;

        if(y1 > y2)
            return y1;
        else
            return y2;
    }


    private void CheckIfBBSlot()
    {
        isBBSlotFree = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.transform.CompareTag("BBSlot"))
                {
                    isBBSlotFree = results[i].gameObject.transform.GetComponent<Slot>().isFree;

                    if(Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_FirstClick)
                    {
                        _pointA = results[i].gameObject;
                    }
                    else if(Globals.mouseClickAction == Globals.MouseClickAction.TwoClicks_SecondClick)
                    {
                        _pointB = results[i].gameObject;
                    }
                }
            }
        }
    }
}