using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverController : MonoBehaviour
{
    public Camera playerCamera;           // The camera the player is using to view the bookshelf
    public float rayDistance = 10f;       // Distance of the ray cast
    public LayerMask interactableLayer;   // Layer to specify which objects are interactable

    private InteractiveObjectControl currentHighlightedObject = null; // Store the current highlighted object

    void Update()
    {
        // Cast a ray from the camera's position to where the mouse is pointing
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an interactable object
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            // Get the InteractiveObjectController component of the hit object
            InteractiveObjectControl highlightedObject = hit.collider.GetComponent<InteractiveObjectControl>();

            // If we are hitting a new object, update the highlight
            if (highlightedObject != null && highlightedObject != currentHighlightedObject)
            {
                // If there's a previously highlighted object, reset it
                if (currentHighlightedObject != null)
                {
                    currentHighlightedObject.HighlightObject(false);
                }

                // Set the new object as highlighted
                currentHighlightedObject = highlightedObject;
                currentHighlightedObject.HighlightObject(true);
            }

            // Handle mouse click on object
            if (Input.GetMouseButtonDown(0) && highlightedObject != null)
            {
                highlightedObject.OnClick(); // trigger objects click behaviour
            }
        }
        else
        {
            // If no object is hit by the ray, reset the previous highlight
            if (currentHighlightedObject != null)
            {
                currentHighlightedObject.HighlightObject(false);
                currentHighlightedObject = null;
            }
        }
    }
}
