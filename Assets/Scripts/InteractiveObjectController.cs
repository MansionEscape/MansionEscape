using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectController : MonoBehaviour
{
    public GameObject player;      // the player object
    public Material highlightMaterial; // highlight material when player is near object
    public Material defaultMaterial;   // orginal default material of object
    private Renderer objectRenderer;   // Renderer for the object to access its materials

    void Start()
    {
        // get the renderer component and store the default material
        objectRenderer = GetComponent<Renderer>();
        defaultMaterial = objectRenderer.material;

    }

    // when player enters trigger area, object material changes to highlight material
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)  // check if object entering is the player
        {
            objectRenderer.material = highlightMaterial;
        }
    }

    // when player leaves the trigger area, object material reverts back to default material
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)    // check if player entering is the player
        {
            objectRenderer.material = defaultMaterial;
        }
    }
}
