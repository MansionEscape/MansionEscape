using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

// Script attaches to the triggers under the bookshelf.
// Allows us to keep track of books in the correct place and adjust the score as needed.
public class CorrectBook : MonoBehaviour
{
    public GameObject book; // The book that will be the correct object for the trigger
    public Material defaultMaterial; // The standard material of the object
    public Material correctMaterial; // The material of the object for when its in the correct position
    public bool inCorrectSpot = false; // bool to keep track of what book is in the right place.


    //Trigger Enter function (when the book enters the trigger)
    void OnTriggerEnter(Collider other)
    {
        // checks if the book that has entered the trigger space is the same as the correct object
        if (other.gameObject == book)
        {
            book.GetComponent<Renderer>().material = correctMaterial; // change colour to green if correct object
            inCorrectSpot = true; // bool is now set to true
             
        }
    }
    //Trigger Exit function (when the book exits the trigger)
    private void OnTriggerExit(Collider other)
    {
        // Checks is the book leaving the area is the correct book
        if(other.gameObject == book)
        {
            // if it is the colour is changed back to the default
            book.GetComponent<Renderer>().material = defaultMaterial;
            inCorrectSpot = false; // bool is chnaged back to indicate the book is no longer in the correct position
            
        }
    }
}
