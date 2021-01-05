using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
 
public class SelectionManager : MonoBehaviour {
 
    public Color highlightMaterial;
    Color originalMaterial;
    GameObject lastHighlightedObject;
    [SerializeField] float rayDistance = 4.0f;
    [SerializeField] private string selectableTag = "Selectable";

    public HUD Hud;
 
    void HighlightObject(GameObject gameObject, Transform selection)
    {
        if (lastHighlightedObject != gameObject)
        {
            ClearHighlighted();
            originalMaterial = gameObject.GetComponent<MeshRenderer>().material.color;
            gameObject.GetComponent<MeshRenderer>().material.color = highlightMaterial;
            lastHighlightedObject = gameObject;
            InteractWithObject(selection);
        }
        InteractWithObject(selection);
    }

    void InteractWithObject(Transform selection)
    {

        if(Globals.menuOpened == false)
        {
            Hud.OpenMessagePanel("open");
        }
        ISelectableItem item = selection.GetComponent<ISelectableItem>();

        if( item != null && Input.GetKeyDown("f"))
        {
            item.onInteract();
            Debug.Log("Interacting with: " + item.Name);
        }
    }
 
    void ClearHighlighted()
    {
        if (lastHighlightedObject != null)
        {
            lastHighlightedObject.GetComponent<MeshRenderer>().material.color = originalMaterial;
            lastHighlightedObject = null;
            Hud.CloseMessagePanel();
            Debug.Log("close colour");
        }
    }
 
    void HighlightObjectInCenterOfCam()
    {
        
        // Ray from the center of the viewport.
        Hud.CloseMessagePanel();
        // Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit rayHit;
        // Check if we hit something.
        if (Physics.Raycast(ray, out rayHit, rayDistance) &&
        Globals.gamePaused == false)
        {
            // Get the object that was hit.

            var selection = rayHit.transform;
            if(selection.CompareTag(selectableTag))
            {
                GameObject hitObject = rayHit.collider.gameObject;
                HighlightObject(hitObject, selection);
            }
            else
            {
                ClearHighlighted();
            }
                
        }
        else
        {
            ClearHighlighted();
        }
        

    }
 
    void Update()
    {
        HighlightObjectInCenterOfCam();
    }
}