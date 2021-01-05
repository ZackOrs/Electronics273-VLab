using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//BAD CODE TO BE REMOVED AFTER SOME TESTING
public class BADSelectionManager : MonoBehaviour
{


    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Color highlightColour = Color.yellow;
    [SerializeField] private Color defaultColor;
    [SerializeField] private float interactDistance = 3.0f;

    public HUD Hud;
    private Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hud.CloseMessagePanel();
        if(_selection != null)
        {
            var selectionRender = _selection.GetComponent<Renderer>();
            selectionRender.material.color = defaultColor;
            _selection = null;
        }


        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag) &&
             (Vector3.Distance(GameObject.Find("Player").transform.position, selection.position) <=interactDistance ) &&
             Globals.gamePaused == false)
            {
                var selectionRender = selection.GetComponent<Renderer>();
                if(selectionRender!= null)
                {
                    selectionRender.material.color = highlightColour;
                    if(Globals.menuOpened == false)
                    {
                        Hud.OpenMessagePanel("open");

                        ISelectableItem item = selection.GetComponent<ISelectableItem>();

                        if( item != null && Input.GetKeyDown("f"))
                        {
                            item.onInteract();
                            Debug.Log("Interacting with: " + item.Name);
                        }
                    }
                }
                _selection = selection;
                
            }
        }
        
    }
}
