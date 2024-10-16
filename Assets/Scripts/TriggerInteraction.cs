using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject player; // Player object
    public GameObject interactiveObject; // Object to highlight.
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;
    public TMP_Text objectText; // text for interaction prompts
    public string interactionMessage; // custom message to display
    public GameObject puzzlePanel; // UI panel of the puzzle
    private bool isPlayerNear = false; // to check if player is in range

    private void Start()
    {
        objectText.text = string.Empty;
        puzzlePanel.SetActive(false);        // ensure panel is hidden at start
    }

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = highlightMaterial;

            if (objectText.name == "BookshelfText")
            {
                objectText.text = "Bookshelf. Press 'E' to interact";
            }
            else if (objectText.name == "PaintingText")
            {
                objectText.text = "Painting. Press 'E' to interact";
            }
            else if (objectText.name == "ChestText")
            {
                objectText.text = "Chest. Press 'E' to interact";
            }
        }          // replaced this code with the code below as it is shorter
        */

        if (other.gameObject == player) 
        {
            interactiveObject.GetComponent<Renderer>().material = highlightMaterial;
            objectText.text = interactionMessage;
            isPlayerNear = true;    
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = defaultMaterial;
            objectText.text = string.Empty;
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // check if player is near and presses 'E'
        {
            Debug.Log("Interaction with object: " + interactiveObject.name); // to check if prompt is working 
            puzzlePanel.SetActive(true);   // display the puzzle panel

        }
    }
}
