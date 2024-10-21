using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

public class CorrectBook : MonoBehaviour
{
    public GameObject book;
    public Material defaultMaterial;
    public Material correctMaterial;
    public bool inCorrectSpot = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == book)
        {
            book.GetComponent<Renderer>().material = correctMaterial;
            inCorrectSpot = true;
             
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == book)
        {
            book.GetComponent<Renderer>().material = defaultMaterial;
            inCorrectSpot = false;
            
        }
    }
}
