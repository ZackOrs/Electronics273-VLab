using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorStyle : MonoBehaviour
{

    [SerializeField] public GameObject cursorA;
    [SerializeField] public GameObject cursorB;

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
            Debug.Log("Enabling CursorA");
            cursorA.SetActive(true);
            cursorA.transform.position = Input.mousePosition;           
        }
        else if(breadbBoardItemSelectedClickCount >= 2)
        {
            Debug.Log("Enabling CursorB");
            cursorA.SetActive(false);
            cursorB.SetActive(true);
            cursorB.transform.position = Input.mousePosition;
        }
        else
        {
            cursorA.SetActive(false);
            cursorB.SetActive(false);
        }
    }
}
