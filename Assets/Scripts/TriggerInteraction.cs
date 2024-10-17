using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject player; // Player object
    public GameObject interactiveObject; // Object you want to change the color of.
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;
    public TMP_Text objectText; // text for interaction prompts
    public string interactionMessage; // custom prompt message to display
    public GameObject puzzlePanel; // UI panel of the puzzle
    private bool isPlayerNear = false; // to check if player is in range



    private void Start()
    {
        objectText.text = string.Empty;
        puzzlePanel.SetActive(false);      // ensure panel is hidden at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = highlightMaterial;
            objectText.text = interactionMessage;   // display the custom message prompt
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
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaction with object: " + interactiveObject.name); // check if prompt is working
            puzzlePanel.SetActive(true); // display the UI puzzle panel

            // Add code here for changing scene if puzzle is on a scene instead of UI.

        }
    }
}
