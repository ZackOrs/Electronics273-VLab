using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCamera : MonoBehaviour
{

    //MoveTowardsTarget variables
    public float moveSpeed = 35.0f;
    public GameObject targetObject;
    public GameObject targetPlayer;
    private bool movingTowardsTarget = false;
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
        if(Input.GetKeyDown("f") && Globals.cameraAttachedToPlayer)
        {
            if(movingTowardsTarget == true)
            {
                movingTowardsTarget = false;
            }
            else
            {
                fromRot = gameObject.transform;
                toRot = targetObject.transform;
                movingTowardsTarget = true;
            }
        }

        if(movingTowardsTarget)
        {
            MoveTowardsTarget(targetObject);
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
                movingTowardsTarget = false;
                inPosition = false;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(fromRot.rotation, toRot.rotation, Time.time * lerpSpeed);
            }
        }
    }

    public void ReturnToPlayer(GameObject player)
    {


    }
}
