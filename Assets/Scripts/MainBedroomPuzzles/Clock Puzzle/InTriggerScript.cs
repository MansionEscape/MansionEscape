using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTriggerScript : MonoBehaviour
{
    public GameObject targetHand;
    public MoveObjects objectHand;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TriggerScript Running");
        objectHand = targetHand.GetComponent<MoveObjects>();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(targetHand.tag))
        {
            objectHand.inTriggerZone = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Out Trigger");
        if (other == targetHand)
        {
            objectHand.inTriggerZone = false;
        }
        
    }
}
