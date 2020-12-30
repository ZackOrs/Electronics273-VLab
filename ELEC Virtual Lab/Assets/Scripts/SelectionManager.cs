using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{


    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
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
            selectionRender.material = defaultMaterial;
            _selection = null;
        }


        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag) &&
             (Vector3.Distance(GameObject.Find("Player").transform.position, selection.position) <=interactDistance ) &&
             PauseMenu.gamePaused == false)
            {
                var selectionRender = selection.GetComponent<Renderer>();
                if(selectionRender!= null)
                {
                    selectionRender.material = highlightMaterial;
                    Hud.OpenMessagePanel("open");

                    ISelectableItem item = selection.GetComponent<ISelectableItem>();

                    if( item != null && Input.GetKeyDown("f"))
                    {
                        Debug.Log("it works");
                    }
                }
                _selection = selection;
                
            }
        }
        
    }
}
