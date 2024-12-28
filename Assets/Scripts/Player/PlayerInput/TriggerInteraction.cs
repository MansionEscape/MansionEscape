using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject player; // Player object
    public GameObject interactiveObject; // Object you want to change the color of.
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;
    public TMP_Text objectText; // text for interaction prompts
    public string interactionMessage; // custom prompt message to display
    public GameObject puzzlePanel; // UI panel of the puzzle
    private bool bookshelfCollision; // to check if player is in range of bookshelf
    private bool chestCollision; // to check if player is in range of chest
    private bool jigsawCollision; // to check if player is in range of jigsaw
    private bool paintingCollision; // to check if player is in range of painting

    public InputActionReference interact;
    private bool wasPressed;


    private void Start()
    {
        chestCollision = false;
        bookshelfCollision = false;
        jigsawCollision = false;
        paintingCollision = false;
        objectText.text = string.Empty;
        puzzlePanel.SetActive(false);      // ensure panel is hidden at start
        wasPressed = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = highlightMaterial;
            objectText.text = interactionMessage;   // display the custom message prompt

            if (interactiveObject.name == "Bookshelf")
            {
                bookshelfCollision = true;
            }
            else if(interactiveObject.name == "Chest")
            {
                chestCollision = true;
            }
            else if(interactiveObject.name == "Jigsaw")
            {
                jigsawCollision = true;
            }
            else if(interactiveObject.name == "PaintingImage")
            {
                paintingCollision = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = defaultMaterial;
            objectText.text = string.Empty;
            if (interactiveObject.name == "Bookshelf")
            {
                bookshelfCollision = false;
            }
            else if (interactiveObject.name == "Chest")
            {
                chestCollision = false;
            }
            else if (interactiveObject.name == "Jigsaw")
            {
                jigsawCollision = false;
            }
            else if (interactiveObject.name == "PaintingImage")
            {
                paintingCollision = false;
            }
        }
    }

    void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();
        
        if (chestCollision && wasPressed)
        {
            Debug.Log("Interaction with object: " + interactiveObject.name); // check if prompt is working
            puzzlePanel.SetActive(true); // display the UI puzzle panel

            // Add code here for changing scene if puzzle is on a scene instead of UI.

        }
        else if (bookshelfCollision && wasPressed)
        {
            SceneManager.LoadScene("BookshelfPuzzle");
        }
        else if (jigsawCollision && wasPressed) 
        {
            SceneManager.LoadScene("JigsawPuzzle");
        }
        else if (paintingCollision && wasPressed)
        {
            Debug.Log(paintingCollision);
            SceneManager.LoadScene("PaintingPuzzle");
        }
    }
}
