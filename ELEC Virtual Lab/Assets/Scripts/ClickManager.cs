using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] GameObject AgilentMachine = null;
    [SerializeField] GameObject PSMachine = null;
    [SerializeField] GameObject FlukeMachine = null;
    [SerializeField] GameObject FGMachine = null;
    [SerializeField] GameObject OscilloscopeMachine = null;

    private string clickableTag = "clickable";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.CompareTag(clickableTag))
                {
                    UseMachine(hit.transform.name);
                    break;
                }
            }
        }
    }

    private void UseMachine(string buttonHit)
    {
        Debug.Log("Clicked machine: "+ Globals.currentMachine);
        switch(Globals.currentMachine)
        {
            case("Agilent"):
            AgilentMachine.GetComponent<AgilentSelect>().ButtonClickHandler(buttonHit);
            break;

            case("Fluke"):
            FlukeMachine.GetComponent<FlukeSelect>().ButtonClickHandler(buttonHit);
            break;

            case("PowerSupply"):
            PSMachine.GetComponent<PSSelect>().ButtonClickHandler(buttonHit);
            break;

            case("FG"):
            break;

            case("Oscilloscope"):
            break;

            default:
            Debug.Log("No Machine");
            break;
        }
    }

}
