using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCamera : MonoBehaviour
{

    //MoveTowardsTarget variables
    public float moveSpeed = 35.0f;
    public GameObject targetObject;
    [SerializeField] GameObject player;
    private bool focusOnTarget = false;
    private bool focusOnPlayer = false;
    private float lerpSpeed = 0.025f;
    private Transform fromRot;
    private Transform toRot;
    private bool inPosition = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //MoveTowards
        if(Input.GetKeyDown("f"))
        {
            Debug.Log("F is pressed: " + focusOnTarget.ToString());
            if(Globals.cameraAttachedToPlayer)
            {
                fromRot = gameObject.transform;
                toRot = targetObject.transform;
                
                inPosition = false;
                focusOnTarget = true;
                focusOnPlayer = false;
            }
            else
            {
                fromRot = gameObject.transform;
                toRot = player.transform;

                inPosition = false;
                focusOnTarget = false;
                focusOnPlayer = true;
            }
        }

        if(focusOnTarget)
        {
            Globals.cameraAttachedToPlayer = false;
            MoveTowardsTarget(targetObject);
            EnableMouse();
        }
        if(focusOnPlayer)
        {
            Globals.cameraAttachedToPlayer = true;
            MoveTowardsTarget(player);
            DisableMouse();
        }
    }

    public void MoveTowardsTarget(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.transform.position) < 0.1)
        {
            transform.position = target.transform.position;
            inPosition = true;
            //movingTowardsTarget = false;
        }

        if(inPosition)
        {
            
            if(Mathf.Abs(fromRot.localEulerAngles.y - toRot.localEulerAngles.y) < 3)
            {
                //reset boolean variables
                transform.rotation = target.transform.rotation;
                focusOnTarget = false;
                focusOnPlayer = false;
                inPosition = false;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(fromRot.rotation, toRot.rotation, Time.time * lerpSpeed);
            }
        }
    }
    private void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
