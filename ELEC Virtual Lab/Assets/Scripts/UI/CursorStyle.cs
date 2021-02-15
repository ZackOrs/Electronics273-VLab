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
        switch (Globals.mouseClickAction)
        {
            case Globals.MouseClickAction.TwoClicks_FirstClick:
                if (!cursorA.activeSelf)
                {
                    cursorA.SetActive(true);
                }
                cursorA.transform.position = CalculateMouseOffset();
                break;
            case Globals.MouseClickAction.TwoClicks_SecondClick:
                if (cursorA.activeSelf)
                {
                    cursorA.SetActive(false);
                    cursorB.SetActive(true);
                }
                cursorB.transform.position = CalculateMouseOffset();
                break;
            default:
                if(cursorA.activeSelf || cursorB.activeSelf)
                {
                    cursorA.SetActive(false);
                    cursorB.SetActive(false);
                }
                break;
        }
    }

    private Vector3 CalculateMouseOffset()
    {
        mouseOffset.x = Input.mousePosition.x + 20.0f;
        mouseOffset.y = Input.mousePosition.y + 20.0f;
        return mouseOffset;
    }
}
