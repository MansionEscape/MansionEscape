using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectControl : MonoBehaviour
{
    public Material highlightMaterial;      // Material for highlighting object
    public Material defaultMaterial;        // Original default material of the object
    public GameObject objectToDisplay;      // Object to display when clicked
    private Renderer objectRenderer;        // Renderer for the object to access its materials

    private void Start()
    {
        // Get the renderer component and store the default material
        objectRenderer = GetComponent<Renderer>();
        defaultMaterial = objectRenderer.material;

        // Initially set the object to display as false
        if (objectToDisplay != null) 
        {
            objectToDisplay.SetActive(false);
        }
    }

    // Method to highlight the object when the mouse is over it
    public void HighlightObject(bool isHighlighted)
    {
        if (isHighlighted)
        {
            objectRenderer.material = highlightMaterial;
        }
        else
        {
            objectRenderer.material = defaultMaterial;
        }
    }

    // Method to handle clicks on the interactive object
    public void OnClick()
    {
        if (objectToDisplay != null)
        {
            objectToDisplay.SetActive(true);
        }
        else 
        {
            Debug.LogWarning("No object assigned to show on click!");
        }
    }
}
