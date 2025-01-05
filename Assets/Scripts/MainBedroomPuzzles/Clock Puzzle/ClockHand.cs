using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    
    public bool correctPos;

    public char control;

    // Angle increment when the clock hand is clicked
    public float rotationAmount = 30f;

    
    void OnMouseDown()
    {

        //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        if (gameObject.transform.rotation.x == 360)
        {
            // Rotate the clock hand by the defined rotationAmount

            transform.Rotate(0, 0, 0); // Negative for clockwise rotation}
        }
        else
        {
            transform.Rotate(0, 0, -rotationAmount);
        }
    }
}
