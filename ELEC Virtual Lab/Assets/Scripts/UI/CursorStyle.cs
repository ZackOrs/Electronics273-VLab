using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorStyle : MonoBehaviour
{

    [SerializeField] public GameObject cursorA;
    [SerializeField] public GameObject cursorB;

    private Vector3 mouseOffset;
    

    public static int breadbBoardItemSelectedClickCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(breadbBoardItemSelectedClickCount == 1)
        {
            
            Debug.Log("X: " + mouseOffset.x);
            Debug.Log("Y: " + mouseOffset.y);
            Debug.Log("Enabling CursorA");
            cursorA.SetActive(true);
            cursorA.transform.position = CalculateMouseOffset();           
        }
        else if(breadbBoardItemSelectedClickCount == 2)
        {
            Debug.Log("Enabling CursorB");
            cursorA.SetActive(false);
            cursorB.SetActive(true);
            cursorB.transform.position = CalculateMouseOffset();
        }
        else if(breadbBoardItemSelectedClickCount > 2)
        {
            breadbBoardItemSelectedClickCount = 0;
        }
        else
        {
            cursorA.SetActive(false);
            cursorB.SetActive(false);
        }
    }


    private Vector3 CalculateMouseOffset()
    {
        mouseOffset.x = Input.mousePosition.x + 20.0f;
        mouseOffset.y = Input.mousePosition.y + 20.0f;
        return mouseOffset;
    }
}
