using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunUIScript : MonoBehaviour
{
    Transform target;
    [SerializeField]
    Vector3 smallSize;
    [SerializeField]
    Vector3 bigSize;
    Vector3 targetSize;
    [SerializeField]
    float circleChangeSpeed;
    [SerializeField]
    HoloController mesh;
    [SerializeField]
    AudioClip interactPromptSound;
    bool selected = false;
    bool inRange = false;
    bool inRaycastSightLeft;
    bool inRaycastSightRight;
    bool leftRaycastSightSelected;
    bool rightRaycastSightSelected;
    float canvasScaler;
    // Use this for initialization
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //set mesh effect off by default and change it if need be
        if (mesh != null)
        mesh.effectOn = false;
        //scale image based on distance so interactable objects are easily noticed
        canvasScaler = (transform.position - Camera.main.transform.position).magnitude;
        if (GetComponentInParent<ObjectState>() != null)
        {
            GetComponent<Image>().enabled = true;
            transform.LookAt(target.position);
            if (GetComponentInParent<ObjectState>().currentState == ObjectState.ObjectStates.Holstered || GetComponentInParent<ObjectState>().currentState == ObjectState.ObjectStates.Free)
            {
                if (inRange || inRaycastSightLeft || inRaycastSightRight)
                {
                    targetSize = bigSize;
                    if (mesh != null)
                        mesh.effectOn = true;
                }
                else
                {
                    targetSize = smallSize;
                    if (mesh != null)
                        mesh.effectOn = false;
                }
            }
            else
            {
                GetComponent<Image>().enabled = false;
            }
        }
        //LERP the image to the target size based on if its in range or not
        transform.localScale = Vector3.Lerp(transform.localScale, targetSize * canvasScaler, Time.deltaTime * circleChangeSpeed);
    }


    public void SetInRange(GameObject hand, bool set)
    {
        //the object can be grabbed, set the UI to become big and do the prompts
        inRange = set;
        //only do selected once
        if (!selected && inRange)
        {
            selected = true;
            hand.GetComponent<MotionController>().InteractPrompt();
            hand.GetComponent<handScript>().PlaySound(interactPromptSound);
        }
        else if (!inRange)
        {
            selected = false;
        }

    }

    public void SetInRaycastSight(GameObject hand, bool set)
    {
        if (hand.GetComponent<MotionController>().hand == MotionController.Hands.LeftHand)
        {
            inRaycastSightLeft = set;
            if (!leftRaycastSightSelected && inRaycastSightLeft)
            {
                leftRaycastSightSelected = true;
                hand.GetComponent<MotionController>().InteractPrompt();
                hand.GetComponent<handScript>().PlaySound(interactPromptSound);
            }
            else if (!inRaycastSightLeft)
            {
                leftRaycastSightSelected = false;
            }
        }
        else
        {
            inRaycastSightRight = set;
            if (!rightRaycastSightSelected && inRaycastSightRight)
            {
                rightRaycastSightSelected = true;
                hand.GetComponent<MotionController>().InteractPrompt();
                hand.GetComponent<handScript>().PlaySound(interactPromptSound);
            }
            else if (!inRaycastSightRight)
            {
                rightRaycastSightSelected = false;
            }
        }
        
    }
}