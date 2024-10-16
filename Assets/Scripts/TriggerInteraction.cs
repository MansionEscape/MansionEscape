using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject player; // Player object
    public GameObject interactiveObject; // Object you want to change the color of.
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;
    public TMP_Text objectText;

    private bool bookshelfCollision;
   


    void Start()
    {
        bookshelfCollision = false;
        Debug.Log("bool is false");
        objectText.text = string.Empty;
    }

    void Update()
    {
        if (bookshelfCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key Pressed and in space");
            SceneManager.LoadScene("BookshelfPuzzle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = highlightMaterial;

            if (objectText.name == "BookshelfText")
            {
                objectText.text = "Bookshelf. Press 'E' to interact";

                Debug.Log("Bool is true");
                 bookshelfCollision = true;

            }
            else if (objectText.name == "PaintingText")
            {
                objectText.text = "Painting. Press 'E' to interact";
             }
            

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            interactiveObject.GetComponent<Renderer>().material = defaultMaterial;
            objectText.text = string.Empty;
        }
    }
}
