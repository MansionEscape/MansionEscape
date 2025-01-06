using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering.Universal;

public class PuzzleController : MonoBehaviour
{
    //public PlayerManager player;

    public bool smallHandInPlace;
    public bool largeHandInPlace;

    public GameObject poster;
    public GameObject smallHand;
    public GameObject largeHand;
    public GameObject targetSmallHand;
    public GameObject targetLargeHand;

    public bool lCorrectPos = false;
    public bool sCorrectPos = false;

    public float threshold = 0.01f;

    public int largeHandTargetRotation = 0;
    public int smallHandTargetRotation = 180;

    public float rotationAmount = 30f;

    public GameObject Compartment;

    public bool puzzleCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        poster.SetActive(true);
        targetSmallHand.SetActive(false);
        targetLargeHand.SetActive(false);
        smallHand.SetActive(true);
        largeHand.SetActive(true);

        if (puzzleCompleted)
        {
            poster.SetActive(true);
            targetSmallHand.SetActive(true);
            targetLargeHand.SetActive(true);
            smallHand.SetActive(false);
            largeHand.SetActive(false);

        }
        
    }


    void Update()
    {
        if (smallHandInPlace && largeHandInPlace)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {

                targetSmallHand.transform.Rotate(0, 0, -rotationAmount);

                if (Mathf.Abs(targetSmallHand.transform.rotation.eulerAngles.z - smallHandTargetRotation) < threshold)
                {
                    sCorrectPos = true;
                }
                else
                {
                    sCorrectPos = false;
                }

            }

            else if (Input.GetKeyDown(KeyCode.L))
            {


                targetLargeHand.transform.Rotate(0, 0, -rotationAmount);

                Debug.Log("Euler:" + targetLargeHand.transform.rotation.eulerAngles.z);
                Debug.Log("Rotation:" + targetLargeHand.transform.rotation.z);

                if (Mathf.Abs(targetLargeHand.transform.rotation.eulerAngles.z - largeHandTargetRotation) < threshold)
                {
                    lCorrectPos = true;
                }
                else
                {
                    lCorrectPos = false;
                }

            }

            if (sCorrectPos && lCorrectPos)
            {
                puzzleCompleted = true;
                Compartment.SetActive(false);
            }


        }

    }
}


    

