using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorPivotController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;


    public GameObject player;

    [SerializeField] private Material HighlightedTrue;
    [SerializeField] private Material HighlightedFalse;

    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";

    //public InteractiveObjectController interactiveObjectController;
    public TriggerInteraction triggerInteraction;

    private bool hasOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            
            if (triggerInteraction.highlightMaterial == HighlightedTrue && !hasOpened)
            {
                myDoor.Play(doorOpen, 0, 0.0f);

                hasOpened = true;
                //gameObject.SetActive(true);
            }

            //if (interactiveObjectController.highlightMaterial == highlightFalse)
            //{
            //    myDoor.Play(doorClose, 0, 0.0f);
            //    gameObject.SetActive(true);
            //}
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {

            if (hasOpened)
            {
                myDoor.Play(doorClose, 0, 0.0f);

                hasOpened = false;
            }
        }

    }

}
