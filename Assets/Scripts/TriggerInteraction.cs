using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject player; // Player object
    public GameObject interactiveObject; // Object you want to change the color of.
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;
    public TMP_Text objectText;


    private void Start()
    {
        objectText.text = string.Empty;
    }

    void OnTriggerEnter(Collider other)
    {
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
